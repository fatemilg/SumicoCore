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
    public partial class Banner : System.Web.UI.Page
    {
        Bis.ContentMethod BisContent = new Bis.ContentMethod();
        Bis.BannerMethod BisBanner = new Bis.BannerMethod();
        Bis.LegalUserMethod BisLegalUser = new Bis.LegalUserMethod();
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.MenuMethod BisMenu = new Bis.MenuMethod();
        public DataRow[] dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multiPart/form-data");
            if (!Page.IsPostBack)
            {

                hfModeFile.Value = "New";
                divInfo.Visible = false;
                fillGrdBanner();
            }
        }

        public void fillGrdBanner()
        {
            ViewModel.Search BannerSearch = new ViewModel.Search();
            BannerSearch.Filter = "ORDER BY tblBanner.Sort";
            DataSet dsBanner = BisBanner.GetBannerData(BannerSearch);
            grdBanner.DataSource = dsBanner;
            grdBanner.DataBind();

        }


        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                ViewModel.tblBanner newBanner = new ViewModel.tblBanner();

                newBanner.IDRet = Guid.Empty;

                newBanner.Name_Fa = txtFileName.Text.FixFarsi();
                newBanner.Name_En = txtFileName_En.Text.FixFarsi();
                newBanner.Description_Fa = txtDescription.Text.FixFarsi();
                newBanner.Description_En = txtDescription_En.Text.FixFarsi();
                newBanner.ForeignLink = RetForeignUrl.Text;
                newBanner.Active = chkActive.Checked;
                newBanner.Sort = txtSort.Text.StringToInt();
                newBanner.Status = 1;

                switch (hfModeFile.Value)
                {
                    case "New":
                        try
                        {
                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Banner/"), "Picture/Banner/", fulPicUrl);
                                if (url != "") newBanner.PicUrl = url;
                                else return;
                            }
                            else
                            {
                                newBanner.PicUrl = "";
                            }

                            if (fulPicUrlForMobile.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Banner/"), "Picture/Banner/", fulPicUrlForMobile);
                                if (url != "") newBanner.PicUrl_Mobile = url;
                                else return;
                            }
                            else
                            {
                                newBanner.PicUrl_Mobile = "";
                            }

                            newBanner.IDBanner = Guid.NewGuid();
                            bool ret = BisBanner.AddBanner(newBanner);
                            if (ret)
                            {
                                txtFileName.Text = txtFileName_En.Text = txtDescription.Text = txtDescription_En.Text = "";
                                chkActive.Checked = true;
                                hfModeFile.Value = "New";
                                hfIDBanner.Value = newBanner.IDBanner.ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد!');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                    case "Edit":
                        try
                        {
                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Banner/"), "Picture/Banner/", fulPicUrl);
                                if (url != "") newBanner.PicUrl = url;
                                else return;

                            }
                            else
                            {
                                newBanner.PicUrl = Session["OldUrlBanner"].ToString();

                            }

                            if (fulPicUrlForMobile.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Banner/"), "Picture/Banner/", fulPicUrlForMobile);
                                if (url != "") newBanner.PicUrl_Mobile = url;
                                else return;

                            }
                            else
                            {
                                newBanner.PicUrl_Mobile = Session["OldUrlBannerMobile"].ToString();

                            }

                            newBanner.IDBanner = hfIDBanner.Value.StringToGuid();

                            bool result = BisBanner.UpdateBanner(newBanner);
                            if (result)
                            {
                                txtFileName.Text = txtFileName_En.Text = txtDescription.Text = txtDescription_En.Text = "";

                                chkActive.Checked = true;
                                if (Session["OldUrlBanner"] != "" && fulPicUrl.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlBanner"].ToString()));
                                }
                                Session.Remove("OldUrlBanner");

                                if (Session["OldUrlBannerMobile"] != "" && fulPicUrlForMobile.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlBannerMobile"].ToString()));
                                }
                                Session.Remove("OldUrlBannerMobile");

                                fillGrdBanner();
                                hfModeFile.Value = "New";
                                imgBanner.Visible = false;
                                imgBannerMobile.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ویرایش شد!');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ویرایش اطلاعات!');", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                }

            }
        }

        protected void grdBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDBanner = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {

                case "Edit":
                    try
                    {
                        Session.Remove("OldUrlBanner");
                        Session.Remove("OldUrlBannerMobile");

                        hfIDBanner.Value = IDBanner.ToString();
                        ViewModel.Search BannerSearch = new ViewModel.Search();
                        BannerSearch.Filter = " and tblBanner.IDBanner ='" + IDBanner + "'";

                        DataSet ds = BisBanner.GetBannerData(BannerSearch);
                        if (!ds.Null_Ds())
                        {

                            RetForeignUrl.Text = ds.ReturnDataSetField("ForeignLink");
                            txtFileName.Text = ds.ReturnDataSetField("Name_Fa");
                            txtFileName_En.Text = ds.ReturnDataSetField("Name_En");
                            txtDescription.Text = ds.ReturnDataSetField("Description_Fa");
                            txtDescription_En.Text = ds.ReturnDataSetField("Description_En");
                            Session["OldUrlBanner"] = ds.ReturnDataSetField("PicUrl");
                            Session["OldUrlBannerMobile"] = ds.ReturnDataSetField("PicUrl_Mobile");

                            chkActive.Checked = ds.ReturnDataSetField("Active").StringToBool();
                            txtSort.Text = ds.ReturnDataSetField("Sort");
                            hfModeFile.Value = "Edit";

                            if (ds.ReturnDataSetField("PicUrl") != "")
                            {
                                imgBanner.Visible = true;
                                imgBanner.ImageUrl = "../" + ds.ReturnDataSetField("PicUrl");
                            }
                            if (ds.ReturnDataSetField("PicUrl_Mobile") != "")
                            {
                                imgBannerMobile.Visible = true;
                                imgBannerMobile.ImageUrl = "../" + ds.ReturnDataSetField("PicUrl_Mobile");
                            }
                            divTable.Visible = false;
                            divInfo.Visible = true;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "Delete":

                    try
                    {
                        ViewModel.tblBanner DeleteBanner = new ViewModel.tblBanner();
                        DeleteBanner.IDBanner = IDBanner;
                        ViewModel.Search DelBannerSearch = new ViewModel.Search();
                        DelBannerSearch.Filter = " and IDBanner = '" + IDBanner.ToString() + "'";
                        DataSet dsBannerSearch = BisBanner.GetBannerData(DelBannerSearch);


                        bool retDel = BisBanner.DeleteBanner(DeleteBanner);
                        if (retDel)
                        {
                            if (dsBannerSearch.ReturnDataSetField("PicUrl") != "")
                            { File.Delete(Server.MapPath("../" + dsBannerSearch.ReturnDataSetField("PicUrl"))); }

                            if (dsBannerSearch.ReturnDataSetField("PicUrl_Mobile") != "")
                            { File.Delete(Server.MapPath("../" + dsBannerSearch.ReturnDataSetField("PicUrl_Mobile"))); }

                            fillGrdBanner();
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;


            }
        }
        protected void grdBanner_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdBanner_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void grdBanner_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBanner.PageIndex = e.NewPageIndex;
            fillGrdBanner();

        }
        protected void btnNewFile_Click(object sender, EventArgs e)
        {
            newFields();
            hfModeFile.Value = "New";

        }
        protected void btnNew2_Click(object sender, EventArgs e)
        {
            divTable.Visible = false;
            divInfo.Visible = true;
            hfModeFile.Value = "New";
            newFields();
        }
        public void newFields()
        {
            txtSort.Text = "0";
            txtFileName.Text = "";
            txtFileName_En.Text = "";
            txtDescription.Text = "";
            txtDescription_En.Text = "";
            imgBanner.Visible = false;
            imgBannerMobile.Visible = false;
            chkActive.Checked = true;
            Session.Remove("OldUrlBanner");
            Session.Remove("OldUrlBannerMobile");
            RetForeignUrl.Text = "";


        }
        protected void btnGrid_Click(object sender, EventArgs e)
        {
            divTable.Visible = true;
            divInfo.Visible = false;
            fillGrdBanner();
        }
        public string UploadFile(string fileUrl, string dbUrl, FileUpload uf)
        {
            string fileType = Path.GetExtension(uf.FileName).ToLower();
            string fileName = Guid.NewGuid().ToString() + fileType;
            int length = uf.PostedFile.ContentLength;

            FileTypes ft = new FileTypes();

            if (ft.imgType().Contains(fileType) || fileType == "")
            {
                if (length <= 4002400)
                {
                    uf.PostedFile.SaveAs(fileUrl + fileName);
                    return dbUrl + fileName;
                }
                if (length > 4002400) //4 مگابایت
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('حجم فایل باید کمتر از 4 مگابایت باشد!');", true);

                }


            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('فایل انتخاب شده در فرمت مجاز نمی باشد!');", true);
            return "";


        }

        protected void delPicBanner_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OldUrlBanner"] == "")
                {
                    return;
                }
                if (Session["OldUrlBanner"] != null)
                {
                    File.Delete(Server.MapPath("../" + Session["OldUrlBanner"].ToString()));
                    Session["OldUrlBanner"] = "";
                }

                fulPicUrl = null;
                imgBanner.Visible = false;

                if (hfIDBanner.Value != "")
                {
                    ViewModel.tblBanner updatePicUrl = new ViewModel.tblBanner();
                    updatePicUrl.PicUrl = "";
                    updatePicUrl.IDBanner = hfIDBanner.Value.StringToGuid();
                    bool ret = BisBanner.UpdateBannerPicUrl(updatePicUrl);
                    if (!ret)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف عکس!');", true);
                    }
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

            }
        }

        public bool ShowImage(string PicUrl)
        {
            if (PicUrl == "")
                return false;
            else
                return true;

        }

        protected void btnDeletePicMobile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OldUrlBannerMobile"] == "")
                {
                    return;
                }
                if (Session["OldUrlBannerMobile"] != null)
                {
                    File.Delete(Server.MapPath("../" + Session["OldUrlBannerMobile"].ToString()));
                    Session["OldUrlBannerMobile"] = "";
                }

                fulPicUrlForMobile = null;
                imgBannerMobile.Visible = false;

                if (hfIDBanner.Value != "")
                {
                    ViewModel.tblBanner updatePicUrl = new ViewModel.tblBanner();
                    updatePicUrl.PicUrl_Mobile = "";
                    updatePicUrl.IDBanner = hfIDBanner.Value.StringToGuid();
                    bool ret = BisBanner.UpdateBannerPicUrlForMobile(updatePicUrl);
                    if (!ret)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف عکس!');", true);
                    }
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

            }
        }
    }
}