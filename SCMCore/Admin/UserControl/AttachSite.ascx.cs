using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;

namespace SCMCore.Admin.UserControl
{
    public partial class AttachSite : System.Web.UI.UserControl
    {
        Bis.AttachSiteMethod bisAttachSiteMethod = new Bis.AttachSiteMethod();
        Bis.AttachCrmInterfaceMethod bisAttachCrmInterfaceMethod = new Bis.AttachCrmInterfaceMethod();
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();
        Bis.AttachInterfaceCategoryMethod BisAttachInterfaceCategory = new Bis.AttachInterfaceCategoryMethod();


        public Guid IDUploadedFile;
        public Guid IDUser;
        public bool SupperUser;
        public string ControlIDAttachSite;


        protected void Page_Init(object sender, EventArgs e)
        {

            IDUser = ((DataSet)Session["User"]).ReturnDataSetField("IDUser").StringToGuid();
            hfIDUser.Value = IDUser.ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlIDAttachSite = "a" + Guid.NewGuid().ToString().Replace("-", "");
                hfControlIDAttachSite.Value = ControlIDAttachSite;
                fileUploadAttachSite.Attributes.Add("onchange", ControlIDAttachSite + "fileUploadAttachSiteOnChange()");
                NewFiledsAttachSite();
                //fillgrdAttachSite();
                FillgrdAttachInterfaceCategory();
                NewAttachInterfaceCategoryFields();
                FillDrpAttachInterfaceCategory();
            }
        }

        protected void btnAddAttachSite_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er1", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>لطفا نام فایل را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }
            switch (hfModeAttachSite.Value)
            {
                case "New":
                    try
                    {
                        bool check = checkAccess(EventName.ListofEvents.AddAttchForProduct.ToString());
                        if (!check)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }

                        if (hfFilelName.Value == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er1", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>لطفا فایل مورد نظر را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }
                        string Prefix = hfFilelName.Value.Substring(hfFilelName.Value.IndexOf('.'), hfFilelName.Value.Length - hfFilelName.Value.IndexOf('.'));
                        if (Prefix == ".doc" || Prefix == ".docx" || Prefix == ".txt" || Prefix == ".pdf" || Prefix == ".rar" || Prefix == ".zip" || Prefix == ".jpg" || Prefix == ".png" || Prefix == ".gif" || Prefix == ".mp4")
                        {
                            hfNewIDAttachSite.Value = Guid.NewGuid().ToString();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UploadFiles1", hfControlIDAttachSite.Value + "UploadFiles('" + fileUploadAttachSite.ClientID + "','File/AttachSite/','" + ((DataSet)Session["User"]).ReturnDataSetField("IDUser") + "');", true);
                            // jahate file grid event btnJustCallForEvent_Click seda zade mishavad
                            fillgrdAttachSite();

                            ///agar hfSituation = single bashad  1 file uplaod mikonad va modal ra mibandad
                            if (hfSituation.Value.ToLower() == "single")
                            {
                                this.Page.GetType().InvokeMember("ChangeAfterUpload", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { hfNewIDAttachSite.Value.StringToGuid(), "File/AttachSite/" + hfNewIDAttachSite.Value.StringToGuid() + Prefix, Prefix, txtTitle.Text, });
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "HideModalAttachSite", "$('#" + ModalAttachSite.ClientID + "').modal('hide');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>فایل انتخابی در فرمت مجاز نمی باشد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

                case "Edit":
                    try
                    {
                        bool check = checkAccess(EventName.ListofEvents.EditAttchForProduct.ToString());
                        if (!check)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }

                        ViewModel.tblAttachSite update = new ViewModel.tblAttachSite();
                        update.IDAttachCrmInterface = hfIDAttachCrmInterface.Value.StringToGuid();
                        update.Name_Fa = txtTitle.Text.FixFarsi();
                        update.Description_Fa = txtDescription.Text.FixFarsi();
                        update.Order = txtOrder.Text.StringToInt();
                        update.CreateDate = DateTime.Now;
                        update.IDAttachInterfaceCategory = drpAttachInterfaceCategory.SelectedValue.StringToGuid();
                        bool ret = bisAttachSiteMethod.UpdateAttachSite(update);
                        if (ret)
                        {
                            fillGrdAttachCrmInterface(hfIDRet.Value, hfPartName.Value);
                            fillgrdAttachSite();
                            NewFiledsAttachSite();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>ویرایش با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در ویرایش!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
            }
        }

        protected void btnAddAttachSiteFromLastFiles_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = checkAccess(EventName.ListofEvents.SelectOldAttchForProduct.ToString());
                if (!check)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                    return;
                }

                string str = "";
                int count = 0;
                foreach (GridViewRow gr in grdAttachSiteFiles.Rows)
                {
                    if (((CheckBox)gr.FindControl("chkSelect")).Checked)
                    {
                        if (count != 0)
                        {
                            str += " or ";
                        }
                        count++;
                        str += " IDAttachSite = '" + ((HiddenField)gr.FindControl("hfIDAttachSite")).Value + "' ";
                    }
                }
                if (count != 0)
                {
                    ViewModel.Search AttachCrmInterfaceRepeatSearch = new ViewModel.Search();
                    AttachCrmInterfaceRepeatSearch.Filter = "(" + str + ") and IDRet = '" + hfIDRet.Value + "' ";
                    DataSet dsRepeatFile = bisAttachCrmInterfaceMethod.GetAttachCrmInterface_GetData_CheckRepeat(AttachCrmInterfaceRepeatSearch);
                    if (dsRepeatFile.Tables[0].Rows.Count == 0)
                    {
                        ViewModel.tblAttachCrmInterface newAttachCrmInterface = new ViewModel.tblAttachCrmInterface();
                        newAttachCrmInterface.AttachSiteCondition = str;
                        newAttachCrmInterface.IDRet = hfIDRet.Value.StringToGuid();

                        bool ret = bisAttachCrmInterfaceMethod.AttachCrmInterface_Insert_FromLastFiles(newAttachCrmInterface);
                        if (ret)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er1", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اطلاعات ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                            fillGrdAttachCrmInterface(hfIDRet.Value, hfPartName.Value);
                            NewFiledsAttachSite();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در ثبت!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>یک یا چند تا از فایلهای انتخابی قبلا انتصاب داده شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>لطفا یک یا چند فایل را انتخاب نمایید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er5", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void grdAttachCrmInterface_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDAttchCrmInterface = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":

                    try
                    {
                        bool check = checkAccess(EventName.ListofEvents.DeleteAttchForProduct.ToString());
                        if (!check)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#" + ModalDeleteWays.ClientID + "').modal('show');$('#" + ModalDeleteWays.ClientID + "').css('overflow','hidden')", true);
                        hfIDAttachCrmInterface.Value = IDAttchCrmInterface.ToString();
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;

                case "Edit":

                    try
                    {
                        ViewModel.Search getAttachCrmInterface = new ViewModel.Search();
                        getAttachCrmInterface.Filter = " and tblAttachCrmInterface.IDAttachCrmInterface =  '" + IDAttchCrmInterface + "'";
                        DataSet ds = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceDataJoinAttachSite(getAttachCrmInterface);
                        txtTitle.Text = ds.ReturnDataSetField("Title");
                        txtDescription.Text = ds.ReturnDataSetField("Description");
                        txtOrder.Text = ds.ReturnDataSetField("Order");
                        hfIDAttachCrmInterface.Value = IDAttchCrmInterface.ToString();
                        drpAttachInterfaceCategory.SelectedValue = ds.ReturnDataSetField("IDAttachInterfaceCategory");
                        hfModeAttachSite.Value = "Edit";
                        //Enable FileUpload ra false mikonad
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UploadFiles", hfControlIDAttachSite.Value + "SetEnableFalse()", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
            }
        }

        protected void grdAttachCrmInterface_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdAttachCrmInterface_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdAttachCrmInterface_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAttachCrmInterface.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsAttachSiteIndex"];
            grdAttachCrmInterface.DataSource = ds;
            grdAttachCrmInterface.DataBind();
        }

        protected void grdAttachCrmInterface_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void fillGrdAttachCrmInterface(string IDRet, string PartName)
        {
            switch (PartName)
            {
                case "Product":
                    ViewModel.Search getAttachProduct = new ViewModel.Search();
                    getAttachProduct.Filter = " and tblAttachCrmInterface.IDRet='" + IDRet + "'";
                    getAttachProduct.Order = " order by tblAttachCrmInterface.[Order] ";
                    DataSet dsAttachProduct = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceData_ProductSite(getAttachProduct);
                    grdAttachCrmInterface.DataSource = dsAttachProduct;
                    grdAttachCrmInterface.DataBind();
                    Session["dsAttachSite"] = dsAttachProduct;
                    Session["dsAttachSiteIndex"] = dsAttachProduct;
                    break;
                case "DefineDetailProduct":
                    ViewModel.Search getAttachDefineDetailProduct = new ViewModel.Search();
                    getAttachDefineDetailProduct.Filter = " and tblAttachCrmInterface.IDRet='" + IDRet + "'";
                    getAttachDefineDetailProduct.Order = " order by tblAttachCrmInterface.[Order] ";
                    DataSet dsAttachDefineDetailProduct = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceData_DefineDetailProductSite(getAttachDefineDetailProduct);
                    grdAttachCrmInterface.DataSource = dsAttachDefineDetailProduct;
                    grdAttachCrmInterface.DataBind();
                    Session["dsAttachSite"] = dsAttachDefineDetailProduct;
                    Session["dsAttachSiteIndex"] = dsAttachDefineDetailProduct;
                    break;
            }

        }

        public void FillgrdAttachInterfaceCategory()
        {
            try
            {
                ViewModel.Search SearchAttachInterfaceCategory = new ViewModel.Search();
                DataSet dsAttachInterfaceCategory = BisAttachInterfaceCategory.GetCategoryData(SearchAttachInterfaceCategory);
                grdAttachInterfaceCategory.DataSource = dsAttachInterfaceCategory;
                grdAttachInterfaceCategory.DataBind();
                Session["dsAttachInterfaceCategory"] = dsAttachInterfaceCategory;
                Session["dsAttachInterfaceCategoryIndex"] = dsAttachInterfaceCategory;
            }
            catch { }
        }

        public void FillDrpAttachInterfaceCategory()
        {
            try
            {
                ViewModel.Search SearchAttachInterfaceCategory = new ViewModel.Search();
                DataSet dsAttachInterfaceCategory = BisAttachInterfaceCategory.GetCategoryData(SearchAttachInterfaceCategory);
                drpAttachInterfaceCategory.DataSource = dsAttachInterfaceCategory;
                drpAttachInterfaceCategory.DataTextField = "Name_En";
                drpAttachInterfaceCategory.DataValueField = "IDAttachInterfaceCategory";
                drpAttachInterfaceCategory.DataBind();
                drpAttachInterfaceCategory.Items.Insert(0, new ListItem("-انتخاب کنید -", Guid.Empty.ToString()));
            }
            catch { }
        }

        public void fillgrdAttachSite()
        {

            //ViewModel.Search AttachSiteSearch = new ViewModel.Search();
            //grdAttachSiteFiles.DataSource = bisAttachSiteMethod.GetAttachSiteData(AttachSiteSearch);
            //grdAttachSiteFiles.DataBind();
        }

        public void setHeaderOfModal(string name)
        {
            lblHeaderOfModal.Text = name;
        }

        public void SetHfIDRetAndMode(string IDRet, string PartName)
        {
            hfIDRet.Value = IDRet;
            hfPartName.Value = PartName;
        }

        public void SetHfIDRetAndMode(string IDRet, string PartName, string Situation)
        {
            hfIDRet.Value = IDRet;
            hfPartName.Value = PartName;
            hfSituation.Value = Situation;
        }

        protected void btnNewAttachSite_Click(object sender, EventArgs e)
        {
            NewFiledsAttachSite();
        }

        public void NewFiledsAttachSite()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";

            hfFilelName.Value = "";
            hfModeAttachSite.Value = "New";

            if (hfIDRet.Value != "")
            {
                ViewModel.Search get = new ViewModel.Search();
                get.Filter = " and tblAttachCrmInterface.IDRet = '" + hfIDRet.Value + "'";
                DataSet ds = bisAttachCrmInterfaceMethod.GetAttachCrmInterface_GetData_MaxOrderByIDRet(get);
                txtOrder.Text = (ds.ReturnDataSetField("MaxOrder").StringToInt() + 1).ToString();
            }


            //foreach (GridViewRow gr in grdAttachSiteFiles.Rows)
            //{
            //    CheckBox chk = (CheckBox)gr.FindControl("chkSelect");
            //    if (chk.Checked)
            //    {
            //        chk.Checked = false;
            //    }
            //}

            //Enable FileUpload ra true mikonad
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UploadFiles", hfControlIDAttachSite.Value + "SetEnableTrue()", true);


        }

        protected void btnJustCallForEvent_Click(object sender, EventArgs e)
        {
            fillGrdAttachCrmInterface(hfIDRet.Value, hfPartName.Value);
            fillgrdAttachSite();
            NewFiledsAttachSite();
        }

        public bool ShowVisible(string IDWriter)
        {
            if (IDUser == IDWriter.StringToGuid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.Search getAttachCrmInterface = new ViewModel.Search();
                getAttachCrmInterface.Filter = " and tblAttachCrmInterface.IDAttachCrmInterface =  '" + hfIDAttachCrmInterface.Value + "'";
                DataSet ds = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceData_getUrlForDeleteInAttachSite(getAttachCrmInterface);
                string root = HostingEnvironment.ApplicationPhysicalPath;
                string filePath = root + ds.ReturnDataSetField("Url");
                if (filePath == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er1", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                ViewModel.tblAttachCrmInterface AttachCrmInterface = new ViewModel.tblAttachCrmInterface();
                AttachCrmInterface.IDAttachCrmInterface = hfIDAttachCrmInterface.Value.StringToGuid();
                bool ret = bisAttachCrmInterfaceMethod.DeleteAllAttachCrmInterfaceByUserForAttachSite(AttachCrmInterface);
                if (ret)
                {
                    File.Delete(filePath);
                    fillGrdAttachCrmInterface(hfIDRet.Value, hfPartName.Value);
                    fillgrdAttachSite();
                    NewFiledsAttachSite();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#" + ModalDeleteWays.ClientID + "').modal('hide');$('#" + ModalDeleteWays.ClientID + "').css('overflow','hidden')", true);

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        protected void btnDeleteJustThis_Click(object sender, EventArgs e)
        {
            try
            {

                ViewModel.tblAttachCrmInterface AttachCrmInterface = new ViewModel.tblAttachCrmInterface();
                AttachCrmInterface.IDAttachCrmInterface = hfIDAttachCrmInterface.Value.StringToGuid();
                bool ret = bisAttachCrmInterfaceMethod.DeleteJustThisAttachCrmInterfaceByUserForAttachSite(AttachCrmInterface);
                if (ret)
                {
                    fillGrdAttachCrmInterface(hfIDRet.Value, hfPartName.Value);
                    fillgrdAttachSite();
                    NewFiledsAttachSite();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#" + ModalDeleteWays.ClientID + "').modal('hide');$('#" + ModalDeleteWays.ClientID + "').css('overflow','hidden')", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er2", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er4", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        protected bool checkAccess(string eventName)
        {
            ViewModel.tblEventUser getUserEvent = new ViewModel.tblEventUser();
            DataSet dsUser = (DataSet)Session["User"];
            getUserEvent.IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
            getUserEvent.EventName = eventName;
            DataSet ds = BisEventUser.CheckForAccessInEventUser(getUserEvent);
            if (ds.Null_Ds())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OpenModalAttachSite()
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#" + ModalAttachSite.ClientID + "').modal('show');", true);

        }

        protected void grdAttachInterfaceCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDAttachInterfaceCategory = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {
                        ViewModel.tblAttachInterfaceCategory DelAttachInterfaceCategory = new ViewModel.tblAttachInterfaceCategory();
                        DelAttachInterfaceCategory.IDAttachInterfaceCategory = IDAttachInterfaceCategory;
                        bool retDel = BisAttachInterfaceCategory.DeleteCategory(DelAttachInterfaceCategory);
                        if (retDel)
                        {
                            FillgrdAttachInterfaceCategory();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

                case "Edit":
                    try
                    {
                        ViewModel.Search getAttachInterfaceCategory = new ViewModel.Search();
                        getAttachInterfaceCategory.Filter = " and tblAttachInterfaceCategory.IDAttachInterfaceCategory =  '" + IDAttachInterfaceCategory + "'";
                        DataSet ds = BisAttachInterfaceCategory.GetCategoryData(getAttachInterfaceCategory);
                        txtAttachInterfaceCategoryNam_Fa.Text = ds.ReturnDataSetField("Name_Fa");
                        txtAttachInterfaceCategoryNam_En.Text = ds.ReturnDataSetField("Name_En");
                        hfIDAttachInterfaceCategory.Value = ds.ReturnDataSetField("IDAttachInterfaceCategory");
                        hfAttachInterfaceCategoryMode.Value = "Edit";
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
            }
        }

        protected void grdAttachInterfaceCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdAttachInterfaceCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdAttachInterfaceCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAttachInterfaceCategory.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsAttachInterfaceCategoryIndex"];
            grdAttachInterfaceCategory.DataSource = ds;
            grdAttachInterfaceCategory.DataBind();
        }

        protected void btnNewAttachInterfaceCategory_Click(object sender, EventArgs e)
        {
            NewAttachInterfaceCategoryFields();
        }

        protected void btnAddAttachInterfaceCategory_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblAttachInterfaceCategory NewAttachInterfaceCategory = new ViewModel.tblAttachInterfaceCategory();
                NewAttachInterfaceCategory.Name_En = txtAttachInterfaceCategoryNam_En.Text.FixFarsi();
                NewAttachInterfaceCategory.Name_Fa = txtAttachInterfaceCategoryNam_Fa.Text.FixFarsi();
                NewAttachInterfaceCategory.Status = 1;

                switch (hfAttachInterfaceCategoryMode.Value)
                {
                    case "New":
                        try
                        {
                            NewAttachInterfaceCategory.IDAttachInterfaceCategory = Guid.NewGuid();
                            bool ret = BisAttachInterfaceCategory.AddCategory(NewAttachInterfaceCategory);
                            if (ret)
                            {
                                FillDrpAttachInterfaceCategory();
                                FillgrdAttachInterfaceCategory();
                                NewAttachInterfaceCategoryFields();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                        break;
                    case "Edit":
                        try
                        {
                            NewAttachInterfaceCategory.IDAttachInterfaceCategory = hfIDAttachInterfaceCategory.Value.StringToGuid();
                            bool result = BisAttachInterfaceCategory.UpdateCategory(NewAttachInterfaceCategory);
                            if (result)
                            {
                                FillDrpAttachInterfaceCategory();
                                FillgrdAttachInterfaceCategory();
                                NewAttachInterfaceCategoryFields();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>ویرایش اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>ویرایش اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "er3", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                        break;
                }

            }
        }

        protected void NewAttachInterfaceCategoryFields()
        {
            txtAttachInterfaceCategoryNam_Fa.Text = txtAttachInterfaceCategoryNam_En.Text = "";
            hfAttachInterfaceCategoryMode.Value = "New";
        }

    }
}