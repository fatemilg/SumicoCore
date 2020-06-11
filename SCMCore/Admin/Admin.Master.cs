using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.Collections;
using System.IO;
using System.Web.Services;

namespace SCMCore.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();
        Bis.AccessLevelMethod BisAccessLevel = new Bis.AccessLevelMethod();
        Bis.ContentMethod BisContent = new Bis.ContentMethod();
        Bis.CommentMethod BisComment = new Bis.CommentMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.DetailTechnicalPropertyMethod BisDetailTechnicalProperty = new Bis.DetailTechnicalPropertyMethod();

        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.UserSiteMethod BisUserSite = new Bis.UserSiteMethod();
        Bis.LogUserMethods BislogUser = new Bis.LogUserMethods();
        public DataTable dtMenu = new DataTable();
        public DataTable dtComment = new DataTable();
        public static string menu;
        Bis.AttachInterfaceCategoryMethod BisCategory = new Bis.AttachInterfaceCategoryMethod();
        DataSet dsUser = new DataSet();
        protected void Page_Init(object sender, EventArgs e)
        {

            if (Request.Cookies["IDLogUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            dsUser = (DataSet)Session["User"];
            if (dsUser.Null_Ds())
            {
                ViewModel.Search getLogUser = new ViewModel.Search();
                getLogUser.Filter = " and tblLogUser.IDLogUser = '" + Request.Cookies["IDLogUser"].Value + "'";
                DataSet dsLoguser = BislogUser.GetLogUserData(getLogUser);

                ViewModel.Search SearchUser = new ViewModel.Search();
                SearchUser.Filter = " and tblUser.IDUser = '" + dsLoguser.ReturnDataSetField("IDUser") + "'";
                dsUser = BisPersonel.GetPersonelData(SearchUser);
                Session["User"] = dsUser;
            }
            lblUserName.Text = dsUser.ReturnDataSetField("FName") + " " + dsUser.ReturnDataSetField("LName");
            if (dsUser.ReturnDataSetField("PicUrl") != "")
            {
                imgUser.ImageUrl = "../" + dsUser.ReturnDataSetField("PicUrl");
            }
            else
            {
                imgUser.ImageUrl = "images/user_male.png";
            }


            if (!dsUser.Null_Ds())
            {
                dtMenu = FillMenu(dsUser.ReturnDataSetField("IDUser").StringToGuid());
                LoadComment();
                top_menu.Visible = dsUser.ReturnDataSetField("PowerUser").StringToBool();
                generateMenu();
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
             
            }
            catch { }
        }
        protected DataTable FillMenu(Guid IDUser)
        {
            ViewModel.Search AccessSearch = new ViewModel.Search();
            AccessSearch.Filter = " And IDUser = '" + IDUser.ToString() + "'  and tblAccessLevel.Access='True' ";
            AccessSearch.Order = " order by tblMenu.[Order] Asc";
            DataSet dsAccessLevel = BisAccessLevel.GetAccessLevelDataForTree(AccessSearch);

            string PageName = HttpContext.Current.Request.Url.AbsolutePath.Substring(7); // esme safhe ra az pusheye admin jodamikonad : /admin
            if (dsAccessLevel.Tables[0].Select("MenuUrl='" + "Admin/" + PageName + "'").Count() == 0 && PageName != "default.aspx")
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                Session["Permission"] = dsAccessLevel.Tables[0].Select("MenuUrl='" + PageName + "'"); // safhei ke vared shodim kolie etleate access level ra be ma midahad rajebe an safhe
            }
            DataRow[] drAccess = dsAccessLevel.Tables[0].Select("ShowInMenuList = 0 ");
            foreach (DataRow row in drAccess)
            {
                dsAccessLevel.Tables[0].Rows.Remove(row);
            }

            return dsAccessLevel.Tables[0];
        }
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Request.Cookies["UserName"].Value = "";
            //Request.Cookies.Remove("IDLogUser");
            Response.Cookies["IDLogUser"].Expires = DateTime.Now;
            Response.Cookies["DomainName"].Expires = DateTime.Now;
            ViewModel.tblLogUser NewlogUser = new ViewModel.tblLogUser();
            NewlogUser.IDLogUser = Guid.NewGuid();
            NewlogUser.IDRet = Guid.Empty;
            NewlogUser.UserAction = "logout";
            NewlogUser.IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
            NewlogUser.IDTableName = Guid.Empty;
            bool ret = BislogUser.AddLogUser(NewlogUser);
            Response.Redirect("Login.aspx");
        }
        protected void LoadComment()
        {
            ViewModel.Search SearchContent = new ViewModel.Search();
            SearchContent.Filter = " and tblComment.Show='false'";
            SearchContent.Order = "order by CreateDate";
            dtComment = BisComment.GetCommentData(SearchContent).Tables[0];
        }
        public string ReturnContentName(string ContentName, string ProductName)
        {
            if (ContentName == "" && ProductName == "")
                return "تماس با ما";
            else if (ContentName != "")
                return " مطلب " + ContentName;
            else
                return " محصول " + ProductName;

        }



        public void generateMenu()
        {
            string str = "<li ><a href=\"default.aspx\" style=\"text-align:right\"><span style=\"margin-right:10px; font-size:17px\"> داشبورد </span> <i class=\"fa fa-dashboard\"></i></a>";
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                if (dtMenu.Rows[i]["ParentID"].ToString() == Guid.Empty.ToString())
                {
                    str += "<li class=\"sub-menu\"><a style=\"text-align:right\" href=\"javascript:;\"><span style=\"margin-right:10px; font-size:17px\">"
                        + dtMenu.Rows[i]["Name_Fa"].ToString()
                        + "</span> <i class=\"fa fa-tasks\"></i></a><ul class=\"sub\">";
                    for (int j = 0; j < dtMenu.Rows.Count; j++)
                    {
                        if (dtMenu.Rows[j]["ParentID"].ToString() == dtMenu.Rows[i]["IDMenu"].ToString())
                        {
                            str += "<li><a style=\"padding-right: 20px; text-align: right; font-size: 16px\" href=\"../" + dtMenu.Rows[j]["MenuUrl"].ToString() + "\">" + dtMenu.Rows[j]["Name_Fa"].ToString() + "<i class=\"fa  fa-arrow-circle-left\" style=\"margin-left:5px\"></i></a></li>";
                        }
                    }
                    str += "</ul></li>";
                }
            }
            menu = str;
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                divMessageChangePass.Visible = false;
                DataSet dsUser = new DataSet();
                dsUser = (DataSet)Session["User"];

                if (dsUser.ReturnDataSetField("Password").DecryptString() == txtOldpass.Text)
                {
                    ViewModel.tblPersonel updatePersonel = new ViewModel.tblPersonel();
                    updatePersonel.Password = txtNewPass.Text.EncryptData();
                    updatePersonel.IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
                    bool ret = BisPersonel.UpdatePersonelChangePass(updatePersonel);
                    if (ret)
                    {
                        divMessageChangePass.Visible = true;
                        lblMessageChangePass.Text = " کلمه عبور ویرایش شد. ";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال درتغییر کلمه عبور!');", true);
                    }

                }
                else
                {
                    divMessageChangePass.Visible = true;
                    lblMessageChangePass.Text = " کلمه عبور فعلی اشتباه است. ";
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            divMessageChangePass.Visible = false;
        }

    }
}