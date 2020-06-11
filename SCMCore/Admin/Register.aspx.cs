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
using System.IO;

namespace SCMCore.Admin
{
    public partial class Register : System.Web.UI.Page
    {
        Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();
        Bis.ContactWayMethod bisContactWay = new Bis.ContactWayMethod();
        public DataRow[] dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multiPart/form-data");


            if (!Page.IsPostBack)
            {
                hfMode.Value = "New";
                divInfo.Visible = false;
                fillGrdUser();
                // ds.Tables[0].Select("FName like '%id%'"); // baraye search
            }
        }
        public void fillGrdUser()
        {
            ViewModel.Search SearchUser = new ViewModel.Search();
            SearchUser.Order = "Order by LName ";
            DataSet dsUser = BisPersonel.GetPersonelData(SearchUser);
            grdUser.DataSource = dsUser;
            grdUser.DataBind();
            Session["dsUser"] = dsUser;
            Session["dsUserIndex"] = dsUser;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblPersonel newPersonel = new ViewModel.tblPersonel();
                //tblUser
                newPersonel.IDCity = Guid.Empty;
                newPersonel.UserName = txtUserName.Text.FixFarsi();
                newPersonel.Password = txtPassword.Text.FixFarsi().EncryptData();
                newPersonel.Email = txtEmail.Text.FixFarsi();
                newPersonel.Address = "";
                newPersonel.WebSite = "";
                newPersonel.PersonelType = true;
                newPersonel.Status = 1;
                //tblPersonel
                newPersonel.FName = txtFName.Text.FixFarsi();
                newPersonel.LName = txtLName.Text.FixFarsi();
                newPersonel.IDOrganizationPosition = Guid.Empty;
                newPersonel.Active = chkActive.Checked;
                newPersonel.PowerUser = chkPowerUser.Checked;
                newPersonel.IdentifyNumber = txtIdentifyNumber.Text.FixFarsi();
                newPersonel.NationalCode = txtNationalCode.Text.FixFarsi();

                switch (hfMode.Value)
                {
                    case "New":
                        try
                        {
                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadImage(Server.MapPath("../Picture/User/"), "Picture/User/", fulPicUrl);
                                if (url != "")
                                {
                                    newPersonel.PicUrl = url;

                                }
                                else return;
                            }
                            else
                            {
                                newPersonel.PicUrl = "";
                            }
                            DataSet dsCheckUserName = (DataSet)Session["User"];
                            int ChekUserCount = dsCheckUserName.Tables[0].Select("UserName = '" + txtUserName.Text + "'").Count();
                            if (ChekUserCount == 0)
                            {
                                newPersonel.IDUser = Guid.NewGuid();
                                bool ret = BisPersonel.AddPersonel(newPersonel);
                                if (ret)
                                {
                                    newFields();
                                    hfMode.Value = "New";
                                    hfIdUser.Value = newPersonel.IDUser.ToString();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد!');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('نام کاربری وارد شده تکراری است');", true);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                    case "Edit":
                        try
                        {
                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadImage(Server.MapPath("../Picture/User/"), "Picture/User/", fulPicUrl);
                                if (url != "")
                                {
                                    newPersonel.PicUrl = url;
                                }
                                else return;
                            }
                            else
                            {
                                newPersonel.PicUrl = Session["OldUrlRegister"].ToString();
                            }

                            newPersonel.IDUser = hfIdUser.Value.StringToGuid();
                            bool result = BisPersonel.UpdatePersonel(newPersonel);
                            if (result)
                            {
                                hfMode.Value = "New";
                                newFields();
                                if (Session["OldUrlRegister"] != "" && fulPicUrl.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlRegister"].ToString()));
                                }
                                Session.Remove("OldUrlRegister");
                                imgUser.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ویرایش شد!');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ویرایش اطلاعات!');", true);

                            }

                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                }
            }
        }


        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDUser = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {

                case "Edit":
                    try
                    {

                        hfIdUser.Value = IDUser.ToString();
                        ViewModel.Search getUser = new ViewModel.Search();
                        getUser.Filter = " and tblPersonel.IDUser ='" + IDUser + "'";

                        DataSet ds = BisPersonel.GetPersonelData(getUser);
                        if (!ds.Null_Ds())
                        {
                            txtFName.Text = ds.ReturnDataSetField("FName");
                            txtLName.Text = ds.ReturnDataSetField("LName");
                            txtUserName.Text = ds.ReturnDataSetField("UserName");
                            txtPassword.Text = ds.ReturnDataSetField("Password").DecryptString();
                            txtEmail.Text = ds.ReturnDataSetField("Email");
                            txtNationalCode.Text = ds.ReturnDataSetField("NationalCode");
                            txtIdentifyNumber.Text = ds.ReturnDataSetField("IdentifyNumber");
                            chkPowerUser.Checked = ds.ReturnDataSetField("PowerUser").StringToBool();
                            chkActive.Checked = ds.ReturnDataSetField("Active").StringToBool();
                            imgUser.Visible = true;
                            imgUser.ImageUrl = "../" + ds.ReturnDataSetField("PicUrl");
                            Session["OldUrlRegister"] = ds.ReturnDataSetField("PicUrl");
                            divTable.Visible = false;
                            divInfo.Visible = true;
                            hfMode.Value = "Edit";
                            txtUserName.Enabled = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
               
            }
        }

        protected void grdUser_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void btnNew2_Click(object sender, EventArgs e)
        {
            divTable.Visible = false;
            divInfo.Visible = true;
            hfMode.Value = "New";
            newFields();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            hfMode.Value = "New";
            newFields();

        }
        public void newFields()
        {
            txtEmail.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtIdentifyNumber.Text = "";
            txtNationalCode.Text = "";
            chkPowerUser.Checked = false;
            imgUser.Visible = false;
            txtUserName.Enabled = true;
         
        }

        protected void btnGrid_Click(object sender, EventArgs e)
        {
            divTable.Visible = true;
            divInfo.Visible = false;
            fillGrdUser();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchPersonel();
        }

        public string UploadImage(string picUrl, string dbUrl, FileUpload uf)
        {
            string imgType = Path.GetExtension(uf.FileName).ToLower();
            string picName = Guid.NewGuid().ToString() + imgType;
            int length = uf.PostedFile.ContentLength;

            if (imgType == ".jpg" || imgType == ".png" || imgType == ".bmp" || imgType == "")
            {
                if (length <= 1024000)
                {
                    uf.PostedFile.SaveAs(picUrl + picName);
                    return dbUrl + picName; // Pic address
                }
                if (length > 1024000) //1 megabyte bar hasbe byte
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('حجم عکس باید کمتر از 1 مگابایت باشد!');", true);

                }


            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('پسوند عکس باید jpg یا png  باشد!');", true);
            return "";


        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;

            DataSet ds = (DataSet)Session["dsUserIndex"];
            grdUser.DataSource = ds;
            grdUser.DataBind();
        }

        public bool ShowImage(string PicUrl)
        {
            if (PicUrl == "")
                return false;
            else
                return true;

        }
        public void searchPersonel()
        {
            DataSet dsSearch = (DataSet)Session["dsUser"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("FName like '%" + txtSearch.Text.FixFarsi() + "%' or LName like '%" + txtSearch.Text.FixFarsi() + "%' or UserName like '%" + txtSearch.Text + "%'"))
            {
                dt.ImportRow(row);
            }
            grdUser.DataSource = dt;  // baraye search
            grdUser.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsUserIndex"] = ds;
        }

        
        
    }
}