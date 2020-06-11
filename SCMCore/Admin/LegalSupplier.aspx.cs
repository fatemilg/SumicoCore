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
using AjaxControlToolkit;

namespace SCMCore.Admin
{
    public partial class LegalSupplier : System.Web.UI.Page
    {
        Bis.LegalUserMethod BisLegalUser = new Bis.LegalUserMethod();
        Bis.RealUserMethod BisRealUser = new Bis.RealUserMethod();
        Bis.AttachInterfaceCategoryMethod BisCategory = new Bis.AttachInterfaceCategoryMethod();
        Bis.CountryMethod bisCountry = new Bis.CountryMethod();
        Bis.StateMethod bisState = new Bis.StateMethod();
        Bis.CityMethod bisCity = new Bis.CityMethod();
        Bis.ContactWayMethod bisContactWay = new Bis.ContactWayMethod();
        Bis.WorkTypeMethod bisWorkType = new Bis.WorkTypeMethod();
        Bis.OrganizationalPositionMethod bisOrganizationPosition = new Bis.OrganizationalPositionMethod();
        Bis.CatalogMethod BisCatalog = new Bis.CatalogMethod();
        string strParentGroup = "";

        public DataRow[] dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multiPart/form-data");
            if (!Page.IsPostBack)
            {
                //btnAddMember.Attributes.Add("OnClientClick", "javascript:return validateMember()");
                btnAdd.Attributes.Add("onclick", "javascript:return validateCompany()");
                //setConfigCkEditor();
                hfMode.Value = "New";
                fillGrdLegalUser();
                divMembers.Visible = false;
                divTableMembers.Visible = false;
                divInfo.Visible = false;
                filltvOrganizationalPosition();
                CasscadDropDownCountryLegalSupplier.fillCountry();
                CasscadDropDownCountryPersonelSupplier.fillCountry();
            }

        }


        public void filltvOrganizationalPosition()
        {
            try
            {

                ViewModel.Search searchOrganizationalPosition = new ViewModel.Search();
                DataSet ds = bisOrganizationPosition.GetOrganizationalPositionData(searchOrganizationalPosition);


                tvOrganizationPost.Nodes.Clear();
                BindTree(ds, null, "IDOrganizationPosition", tvOrganizationPost);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }

        }

        private void BindTree(DataSet ds, TreeNode parentNode, string ID, TreeView tv)
        {

            DataRow[] ChildRows;
            if (parentNode == null)
            {
                string strExpr = "ParentID = '" + Guid.Empty.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            else
            {
                string strExpr = "ParentID = '" + parentNode.Value.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            foreach (DataRow dr in ChildRows)
            {
                TreeNode newNode = new TreeNode(dr["Name_Fa"].ToString(), dr[ID].ToString());
                if (parentNode == null)
                {
                    tv.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(ds, newNode, ID, tv);
            }

        }

        public void fillGrdLegalUser()
        {
            ViewModel.Search LegalUserSearch = new ViewModel.Search();
            DataSet dsLegalUser = BisLegalUser.GetSupplierData(LegalUserSearch);
            grdLegalUser.DataSource = dsLegalUser;
            grdLegalUser.DataBind();
            Session["dsLegalUser"] = dsLegalUser;
            Session["dsLegalUserIndex"] = dsLegalUser;

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            ViewModel.tblLegalUser newLegalUser = new ViewModel.tblLegalUser();
            //tblUser
            newLegalUser.IDCity = CasscadDropDownCountryLegalSupplier.IDCity.StringToGuid();
            newLegalUser.UserName = "";
            newLegalUser.Password = "";
            newLegalUser.Address = "";
            newLegalUser.WebSite = txtWebSite.Text.FixFarsi();
            newLegalUser.SupplierType = true;
            newLegalUser.Active = chkActive.Checked;

            //tblLegalUser
            newLegalUser.Name_Fa = txtName.Text.FixFarsi();
            newLegalUser.Name_En = txtName_En.Text.FixFarsi();
            newLegalUser.Description_Fa = txtDescription.Text.FixFarsi();
            newLegalUser.Description_En = txtDescription_En.Text.FixFarsi();
            newLegalUser.PostalCode = txtPostalCode.Text.FixFarsi();
            newLegalUser.RegistrationCode = txtRegistrationCode.Text.FixFarsi();
            newLegalUser.NationalCode = txtNationalCode.Text.FixFarsi();
            newLegalUser.RegistrationNumber = txtRegistrationNumber.Text.StringToInt();
            newLegalUser.IDParentCompany = hfParentCompany.Value.StringToGuid();
            newLegalUser.MetaTag = txtMetaTag.Text.FixFarsi();

            newLegalUser.Status = 1;


            switch (hfMode.Value)
            {
                case "New":
                    try
                    {
                        if (fulPicUrl.FileName != "")
                        {
                            string url = UploadFile(Server.MapPath("../Picture/Company/"), "Picture/Company/", fulPicUrl);
                            if (url != "") newLegalUser.PicUrl = url;
                            else return;

                        }
                        else
                        {
                            newLegalUser.PicUrl = "";

                        }
                        Guid IDUser = Guid.NewGuid();
                        newLegalUser.IDUser = IDUser;
                        if (checkNameOfCompany(txtName.Text.FixFarsi(), ""))
                        {
                            bool ret = BisLegalUser.AddSupplier(newLegalUser);
                            if (ret)
                            {
                                newFields();
                                hfMode.Value = "New";
                                hfIDLegalUser.Value = newLegalUser.IDUser.ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد - ثبت اطلاعات تماس و پرسنل فراموش نشود!');", true);

                                // -- popupe tel baz mishavad
                                ViewModel.Search LegalUserSearch = new ViewModel.Search();
                                LegalUserSearch.Filter = " and tblLegalUser.IDUser ='" + IDUser.ToString() + "'";
                                DataSet ds = BisLegalUser.GetSupplierData(LegalUserSearch);
                                fillGrdLegalUser();
                                hfIDLegalUser.Value = IDUser.ToString();
                                hfParentCompany.Value = "";

                                divTable.Visible = true;
                                divInfo.Visible = false;
                                //--

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('کمپانی با این نام قبلا ثبت شده است!');", true);

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
                            string url = UploadFile(Server.MapPath("../Picture/Company/"), "Picture/Company/", fulPicUrl);
                            if (url != "") newLegalUser.PicUrl = url;
                            else return;

                        }
                        else
                        {
                            newLegalUser.PicUrl = Session["OldUrlSupplier"].ToString();

                        }

                        newLegalUser.IDUser = hfIDLegalUser.Value.StringToGuid();
                        if (checkNameOfCompany(txtName.Text.FixFarsi(), hfIDLegalUser.Value))
                        {
                            bool result = BisLegalUser.UpdateSupplier(newLegalUser);
                            if (result)
                            {


                                if (Session["OldUrlSupplier"] != "" && fulPicUrl.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlSupplier"].ToString()));
                                }
                                Session.Remove("OldUrlSupplier");
                                newFields();
                                hfParentCompany.Value = "";
                                hfMode.Value = "New";
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ویرایش شد!');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ویرایش اطلاعات!');", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('کمپانی با این نام قبلا ثبت شده است!');", true);

                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                    }

                    break;
            }


        }

        public string ShowSex(bool sex)
        {
            if (sex == false) return "زن";
            else return "مرد";
        }
        protected void grdLegalUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDLegalUser = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        txtBaseCompany.Enabled = false;
                        divMembers.Visible = false;
                        divTableMembers.Visible = false;
                        hfIDLegalUser.Value = IDLegalUser.ToString();
                        ViewModel.Search LegalUserSearch = new ViewModel.Search();
                        LegalUserSearch.Filter = " and tblLegalUser.IDUser ='" + IDLegalUser + "'";

                        DataSet ds = BisLegalUser.GetSupplierData(LegalUserSearch);
                        if (!ds.Null_Ds())
                        {
                            CasscadDropDownCountryLegalSupplier.SetDropDownsFromEdit(ds.ReturnDataSetField("IDCity"));
                            txtName.Text = ds.ReturnDataSetField("Name_Fa");
                            txtName_En.Text = ds.ReturnDataSetField("Name_En");
                            chkActive.Checked = ds.ReturnDataSetField("Active").StringToBool();
                            txtDescription.Text = ds.ReturnDataSetField("Description_Fa");
                            txtDescription_En.Text = ds.ReturnDataSetField("Description_En");
                            txtWebSite.Text = ds.ReturnDataSetField("WebSite");
                            txtPostalCode.Text = ds.ReturnDataSetField("PostalCode");
                            txtRegistrationCode.Text = ds.ReturnDataSetField("RegistrationCode");
                            txtNationalCode.Text = ds.ReturnDataSetField("NationalCode");
                            txtRegistrationNumber.Text = ds.ReturnDataSetField("RegistrationNumber");
                            txtBaseCompany.Text = ds.ReturnDataSetField("ParentCompanyName");
                            hfParentCompany.Value = ds.ReturnDataSetField("IDParentCompany");
                            txtMetaTag.Text = ds.ReturnDataSetField("MetaTag");
                            Session["OldUrlSupplier"] = ds.ReturnDataSetField("PicUrl");
                            hfMode.Value = "Edit";
                            imgLegalUser.Visible = true;
                            imgLegalUser.ImageUrl = "../" + ds.ReturnDataSetField("PicUrl");

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

                case "Tel":
                    try
                    {
                        //GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        //ModalPopupExtender mpe = new ModalPopupExtender();
                        //mpe = (ModalPopupExtender)row.FindControl("mp1");
                        //drpTelType = (DropDownList)mpe.FindControl("drpTelType");
                        //grdContactWay = (GridView)mpe.FindControl("grdContactWay");

                        ViewModel.Search LegalUserSearch = new ViewModel.Search();
                        LegalUserSearch.Filter = " and tblLegalUser.IDUser ='" + IDLegalUser + "'";
                        DataSet ds = BisLegalUser.GetSupplierData(LegalUserSearch);

                        hfIDLegalUser.Value = IDLegalUser.ToString();
                    }
                    catch
                    {

                    }
                    break;
                case "Member":
                    try
                    {
                        divTableMembers.Visible = true;
                        divMembers.Visible = false;
                        grdMember.DataSource = null;
                        grdMember.DataBind();
                        hfIDLegalUser.Value = IDLegalUser.ToString();
                        fillMemberInCompany(hfIDLegalUser.Value.StringToGuid());

                        ViewModel.Search LegalUserSearch = new ViewModel.Search();
                        LegalUserSearch.Filter = " and tblLegalUser.IDUser ='" + IDLegalUser + "'";

                        DataSet ds = BisLegalUser.GetSupplierData(LegalUserSearch);
                        if (!ds.Null_Ds())
                        {
                            lblCompanyForMember.Text = ds.ReturnDataSetField("Name_Fa");
                            lblCompany.Text = ds.ReturnDataSetField("Name_Fa");
                        }
                    }
                    catch
                    {

                    }
                    break;
                case "Product":
                    {
                        Session["IDSupplierForProductPage"] = IDLegalUser.ToString();
                        Response.Redirect("Product.aspx");
                    }
                    break;
                case "Accessory":
                    {
                        Session["IDSupplierForAccessoryPage"] = IDLegalUser.ToString();
                        Response.Redirect("Accessory.aspx");
                    }
                    break;
                case "Dependency":
                    {
                        try
                        {
                            hfIDLegalUser.Value = IDLegalUser.ToString();
                            ViewModel.Search LegalUserSearch = new ViewModel.Search();
                            LegalUserSearch.Filter = " and tblLegalUser.IDUser ='" + IDLegalUser + "'";
                            DataSet ds = BisLegalUser.GetSupplierData(LegalUserSearch);
                            if (ds.ReturnDataSetField("MenuPicUrl") != "")
                            {
                                imgOldSupplierMenuPic.Visible = false;
                                imgOldSupplierMenuPic.ImageUrl = @"\" + ds.ReturnDataSetField("MenuPicUrl");
                            }
                            FillGrdSupplierCatalog(IDLegalUser);
                            imgOldSupplierMenuPic.ImageUrl = @"\" + ds.ReturnDataSetField("MenuPicUrl");
                            lblSupplierName.Text = ds.ReturnDataSetField("Name_Fa");
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalSupplierDependency').modal('show');", true);
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                        }
                    }
                    break;
            }
        }

        protected void grdLegalUser_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdLegalUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdLegalUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLegalUser.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsLegalUserIndex"];
            grdLegalUser.DataSource = ds;
            grdLegalUser.DataBind();

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            newFields();
            hfMode.Value = "New";

        }

        protected void btnNew2_Click(object sender, EventArgs e)
        {
            divTable.Visible = false;
            divInfo.Visible = true;
            hfMode.Value = "New";
            newFields();
        }

        public void newFields()
        {

            txtName.Text = "";
            txtName_En.Text = "";
            txtDescription.Text = "";
            txtDescription_En.Text = "";
            txtPostalCode.Text = "";
            txtNationalCode.Text = "";
            txtRegistrationCode.Text = "";
            txtWebSite.Text = "";
            txtRegistrationNumber.Text = "";
            chkActive.Checked = true;
            CasscadDropDownCountryLegalSupplier.CleanDropDowns();

            fulPicUrl = null;
            divMembers.Visible = false;
            divTableMembers.Visible = false;
            txtBaseCompany.Text = "";
            txtBaseCompany.Enabled = true;
            hfParentCompany.Value = "";
            imgLegalUser.Visible = false;
        }

        protected void btnGrid_Click(object sender, EventArgs e)
        {
            divMembers.Visible = false;
            divTableMembers.Visible = false;
            divTable.Visible = true;
            divInfo.Visible = false;
            fillGrdLegalUser();
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

        public bool checkNameOfCompany(string name, string ID)
        {
            ViewModel.Search searchName = new ViewModel.Search();
            if (ID != "")
            {
                searchName.Filter = " and tblLegalUser.Name_Fa=N'" + name + "' and tblLegalUser.IDUser <>'" + ID + "'";
            }
            else
            {
                searchName.Filter = " and tblLegalUser.Name_Fa=N'" + name + "'";
            }
            DataSet ds = BisLegalUser.GetSupplierData(searchName);

            return ds.Null_Ds(); //قبلا با این نام شرکت ثبت نشده است

        }



        //تنظیمات CkEditor
        //public void setConfigCkEditor()
        //{
        //    txtDescription_EnRealUser.config.height = "100px";
        //    txtDescriptionRealUser.config.height = "100px";
        //    txtDescription_EnRealUser.config.toolbar = new object[]
        //            {
        //                new object[] { "Source" ,"Preview","Print","Undo","Redo","JustifyLeft","JustifyRight","JustifyCenter","JustifyBlock","-","BidiRtl","BidiLtr"},
        //                new object[] { "Bold", "Italic", "Underline", "Strike"},
        //                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent","Link","Unlink" },
        //                new object[] {  "FontSize", "TextColor", "BGColor", "-" ,"Image"},
        //            };
        //    txtDescriptionRealUser.config.toolbar = new object[]
        //            {
        //                new object[] { "Source" ,"Preview","Print","Undo","Redo","JustifyLeft","JustifyRight","JustifyCenter","JustifyBlock","-","BidiRtl","BidiLtr"},
        //                new object[] { "Bold", "Italic", "Underline", "Strike"},
        //                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent","Link","Unlink" },
        //                new object[] {  "FontSize", "TextColor", "BGColor", "-" ,"Image"},
        //            };

        //}

        protected void btnNewMemberForCompany_Click(object sender, EventArgs e)
        {
            newFieldsMembers();
            hfModeRealUser.Value = "New";
            divTableMembers.Visible = false;
            divMembers.Visible = true;
        }

        public void fillMemberInCompany(Guid IDUser)
        {
            ViewModel.Search searchRealUser = new ViewModel.Search();
            searchRealUser.Filter = " and tblRealUser.IDLegalUser='" + IDUser + "'";
            DataSet ds = BisRealUser.GetRealUserSupplierData(searchRealUser);

            grdMember.DataSource = ds;
            grdMember.DataBind();
            Session["dsRealUser"] = ds;
            Session["dsRealUserIndex"] = ds;


        }

        protected void btnNewMember_Click(object sender, EventArgs e)
        {
            newFieldsMembers();
            hfModeRealUser.Value = "New";
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ViewModel.tblRealUser newRealUser = new ViewModel.tblRealUser();
                //tblUser
                newRealUser.IDCity = CasscadDropDownCountryPersonelSupplier.IDCity.StringToGuid();
                newRealUser.UserName = "";
                newRealUser.Password = "";
                newRealUser.Address = "";
                newRealUser.WebSite = txtMemberWebsite.Text.FixFarsi();
                newRealUser.SupplierType = true;
                newRealUser.Active = chkMemberActive.Checked;

                //tblRealUser
                newRealUser.IDLegalUser = hfIDLegalUser.Value.StringToGuid();
                newRealUser.FName = txtFName.Text.FixFarsi();
                newRealUser.LName = txtLName.Text.FixFarsi();
                newRealUser.Description_Fa = txtDescriptionRealUser.Text.FixFarsi();
                newRealUser.Description_En = txtDescription_EnRealUser.Text.FixFarsi();
                newRealUser.IdentifyNumber = txtMemberIdentifyNumber.Text.FixFarsi();
                newRealUser.NationalCode = txtMemberMeliCode.Text.FixFarsi();
                newRealUser.IDWorkType = Guid.Empty;
                if (tvOrganizationPost.SelectedNode != null)
                {
                    newRealUser.IDOrganizationPosition = tvOrganizationPost.SelectedNode.Value.StringToGuid();
                }
                else
                {
                    newRealUser.IDOrganizationPosition = Guid.Empty;
                }

                if (rbSex.SelectedValue == "0")
                {
                    newRealUser.Sex = false; //مرد
                }
                else if (rbSex.SelectedValue == "1")
                {
                    newRealUser.Sex = true; //زن
                }
                newRealUser.Status = 1;

                switch (hfModeRealUser.Value)
                {
                    case "New":
                        try
                        {
                            Guid IDUser = Guid.NewGuid();
                            newRealUser.IDUser = IDUser;

                            bool ret = BisRealUser.AddRealSupplier(newRealUser);
                            if (ret)
                            {
                                newFieldsMembers();
                                hfModeRealUser.Value = "New";
                                hfIDRealUser.Value = newRealUser.IDUser.ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد - ثبت اطلاعات تماس برای پرسنل فراموش نشود!');", true);

                                ViewModel.Search RealUserSearch = new ViewModel.Search();
                                RealUserSearch.Filter = " and tblRealUser.IDUser ='" + IDUser + "'";
                                DataSet ds = BisRealUser.GetRealUserSupplierData(RealUserSearch);

                                hfIDRealUser.Value = IDUser.ToString();
                                hfModePhoneMember.Value = "New";
                                divTableMembers.Visible = true;
                                divMembers.Visible = false;
                                fillMemberInCompany(hfIDLegalUser.Value.StringToGuid());

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

                            newRealUser.IDUser = hfIDRealUser.Value.StringToGuid();

                            bool result = BisRealUser.UpdateRealSupplier(newRealUser);
                            if (result)
                            {
                                fillMemberInCompany(hfIDLegalUser.Value.StringToGuid());
                                newFieldsMembers();
                                hfModeRealUser.Value = "New";
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

        public void newFieldsMembers()
        {
            ViewModel.Search searchCompanyFormember = new ViewModel.Search();
            searchCompanyFormember.Filter = " and tblLegalUser.IDUser = '" + hfIDLegalUser.Value + "'";
            DataSet ds = BisLegalUser.GetSupplierData(searchCompanyFormember);
            CasscadDropDownCountryPersonelSupplier.CleanDropDowns();
            txtFName.Text = "";
            txtLName.Text = "";
            txtDescriptionRealUser.Text = "";
            txtDescription_EnRealUser.Text = "";

            txtMemberMeliCode.Text = "";
            txtMemberIdentifyNumber.Text = "";
            txtMemberWebsite.Text = "";
            lblOrganizationPost.Text = "";
            if (tvOrganizationPost.SelectedNode != null)
            {
                tvOrganizationPost.SelectedNode.Selected = false;
            }
            chkMemberActive.Checked = true;
            rbSex.SelectedValue = "0";//مرد
            divMembers.Visible = true;
            divTableMembers.Visible = false;

        }

        protected void grdMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDRealUser = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        divMembers.Visible = true;
                        divTableMembers.Visible = false;

                        hfIDRealUser.Value = IDRealUser.ToString();
                        ViewModel.Search RealUserSearch = new ViewModel.Search();
                        RealUserSearch.Filter = " and tblRealUser.IDUser ='" + IDRealUser + "'";

                        DataSet ds = BisRealUser.GetRealUserSupplierData(RealUserSearch);
                        if (!ds.Null_Ds())
                        {
                            CasscadDropDownCountryPersonelSupplier.SetDropDownsFromEdit(ds.ReturnDataSetField("IDCity"));

                            txtFName.Text = ds.ReturnDataSetField("FName");
                            txtLName.Text = ds.ReturnDataSetField("LName");
                            chkMemberActive.Checked = ds.ReturnDataSetField("Active").StringToBool();
                            txtDescriptionRealUser.Text = ds.ReturnDataSetField("Description_Fa");
                            txtDescription_EnRealUser.Text = ds.ReturnDataSetField("Description_En");
                            txtMemberWebsite.Text = ds.ReturnDataSetField("WebSite");
                            txtMemberIdentifyNumber.Text = ds.ReturnDataSetField("IdentifyNumber");
                            txtMemberMeliCode.Text = ds.ReturnDataSetField("NationalCode");
                            lblOrganizationPost.Text = GenerateOrganizationalPosition(ds.ReturnDataSetField("IDOrganizationPosition"));
                            tvOrganizationPost.SelectedNodeExtension(ds.ReturnDataSetField("IDOrganizationPosition"));

                            if (ds.ReturnDataSetField("Sex") == "True")
                            {
                                rbSex.SelectedValue = "1";//زن
                            }
                            else
                            {
                                rbSex.SelectedValue = "0"; //مرد
                            }
                            hfModeRealUser.Value = "Edit";
                            divMembers.Visible = true;
                            divTableMembers.Visible = false;

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


            }
        }

        protected void grdMember_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMember.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsRealUserIndex"];
            grdMember.DataSource = ds;
            grdMember.DataBind();
        }

        protected void btnGridMember_Click(object sender, EventArgs e)
        {
            divMembers.Visible = false;
            divTableMembers.Visible = true;
            fillMemberInCompany(hfIDLegalUser.Value.StringToGuid());
        }



        protected void btnSearchCompany_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsLegalUser"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("Name_Fa like '%" + txtSearchCompany.Text.FixFarsi() + "%' or Name_En like '%" + txtSearchCompany.Text + "%'"))
            {
                dt.ImportRow(row);
            }
            grdLegalUser.DataSource = dt;  // baraye search
            grdLegalUser.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsLegalUserIndex"] = ds;
        }

        protected void btnSearchMember_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsRealUser"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("FName like '%" + txtSearchMember.Text.FixFarsi() + "%' or LName like '%" + txtSearchMember.Text + "%'"))
            {
                dt.ImportRow(row);
            }
            grdMember.DataSource = dt;  // baraye search
            grdMember.DataBind();

        }



        public string GenerateOrganizationalPosition(string ParentID)
        {
            ViewModel.Search searchOrganizationPosition = new ViewModel.Search();
            DataSet ds = bisOrganizationPosition.GetOrganizationalPositionData(searchOrganizationPosition);
            if (ds.Tables[0].Select("IDOrganizationPosition='" + ParentID + "'").Count() > 0)
            {
                strParentGroup += " <i class=\"fa fa-arrow-left\"> </i> " + ds.Tables[0].Select("IDOrganizationPosition='" + ParentID + "'")[0]["Name_Fa"].ToString();
                GenerateOrganizationalPosition(ds.Tables[0].Select("IDOrganizationPosition='" + ParentID.ToString() + "'")[0]["ParentID"].ToString());
            }
            return strParentGroup;
        }

        protected void grdLegalUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            strParentGroup = "";
        }

        protected void tvOrganizationPost_SelectedNodeChanged(object sender, EventArgs e)
        {

            lblOrganizationPost.Text = GenerateOrganizationalPosition(tvOrganizationPost.SelectedNode.Value);
        }

        protected void grdMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            strParentGroup = "";
        }


        protected void RemovePostOrganization_Click(object sender, EventArgs e)
        {
            lblOrganizationPost.Text = "";
            if (tvOrganizationPost.SelectedNode != null)
            {
                tvOrganizationPost.SelectedNode.Selected = false;
            }
        }

        protected void btnSaveSupplierMenuPic_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblLegalUser UpdateLegalUser = new ViewModel.tblLegalUser();

                //tblSupplier
                UpdateLegalUser.IDUser = hfIDLegalUser.Value.StringToGuid();
                UpdateLegalUser.MenuPicUrl = FileUploadMenuPicUrl.MoveFile(@"..\Picture\Company");
                bool result = BisLegalUser.UpdateSupplier(UpdateLegalUser);
                if (result)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalSupplierDependency').modal('hide');", true);
                }
                else
                {
                    try { File.Delete(Server.MapPath(@"\" + UpdateLegalUser.MenuPicUrl)); }
                    catch { }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch
            {

            }
        }

        protected void btnDelOldSupplierMenuPic_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblLegalUser UpdateLegalUser = new ViewModel.tblLegalUser();

                //tblSupplier
                UpdateLegalUser.IDUser = hfIDLegalUser.Value.StringToGuid();
                UpdateLegalUser.MenuPicUrl = "";
                bool result = BisLegalUser.UpdateSupplier(UpdateLegalUser);
                if (result)
                {
                    try
                    {
                        File.Delete(Server.MapPath(@"\" + imgOldSupplierMenuPic.ImageUrl));
                        imgOldSupplierMenuPic.Visible = false;
                    }
                    catch { }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch
            {

            }
        }

        protected void grdSupplierCatalog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdSupplierCatalog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDCatalog = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {


                case "Delete":

                    try
                    {
                        bool retDel = true;
                        ViewModel.Search SearchCatalog = new ViewModel.Search();
                        SearchCatalog.Filter = " AND tblCatalog.IDCatalog = '" + IDCatalog + "'";
                        DataSet dsCatalog = BisCatalog.GetCatalogData(SearchCatalog);

                        try
                        {
                            File.Delete(Server.MapPath("/" + dsCatalog.ReturnDataSetField("PDFUrl")));
                        }
                        catch
                        {
                            retDel = false;
                        }
                        try
                        {
                            File.Delete(Server.MapPath("/" + dsCatalog.ReturnDataSetField("PicUrl")));
                        }
                        catch
                        {
                            retDel = false;
                        }
                        if (retDel)
                        {
                            ViewModel.tblCatalog Delete = new ViewModel.tblCatalog();
                            Delete.IDCatalog = IDCatalog;
                            retDel = BisCatalog.DeleteCatalog(Delete);

                            if (retDel)
                            {
                                FillGrdSupplierCatalog(hfIDLegalUser.Value.StringToGuid());
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                            }
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

        protected void grdSupplierCatalog_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdSupplierCatalog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnCatalogPic_Click(object sender, ImageClickEventArgs e)
        {
            //if (hf)
            FileUploadAttachSiteForCatalog.SetHfIDRetAndMode(null, "CatalogPic", "single");
            FileUploadAttachSiteForCatalog.setHeaderOfModal("آپلود تصویر کاتالوگ");
            FileUploadAttachSiteForCatalog.NewFiledsAttachSite();
            FileUploadAttachSiteForCatalog.fillGrdAttachCrmInterface(null, "Catalog");
            FileUploadAttachSiteForCatalog.fillgrdAttachSite();
            FileUploadAttachSiteForCatalog.OpenModalAttachSite();
            hfUploadTo.Value = "CatalogPic";
        }

        public void ChangeAfterUpload(Guid IDUploadedFile, string FileName, string Perfix, string Name)
        {
            FileTypes fp = new FileTypes();
            if (fp.imgType().Contains(Perfix))
            {
                ((Image)UpdatePanel8.FindControl("btn" + hfUploadTo.Value)).ImageUrl = "/" + FileName;
                lblCatalogPicName.Text = Name;
            }
            else
            {
                lblAttachCatalogFileName.Text = Name;
            }
            //btDeleteCatalogPic.Visible = true;
            ((HiddenField)UpdatePanel8.FindControl("hfID" + hfUploadTo.Value)).Value = IDUploadedFile.ToString();
        }

        protected void btnAttachCatalogFile_Click(object sender, ImageClickEventArgs e)
        {
            FileUploadAttachSiteForCatalog.SetHfIDRetAndMode(null, "AttachCatalogFile", "single");
            FileUploadAttachSiteForCatalog.setHeaderOfModal("آپلود فایل کاتالوگ");
            FileUploadAttachSiteForCatalog.NewFiledsAttachSite();
            FileUploadAttachSiteForCatalog.fillGrdAttachCrmInterface(null, "Catalog");
            FileUploadAttachSiteForCatalog.fillgrdAttachSite();
            FileUploadAttachSiteForCatalog.OpenModalAttachSite();
            hfUploadTo.Value = "AttachCatalogFile";
        }

    

        protected void btnSaveCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblAttachCatalogFileName.Text == "" || lblCatalogPicName.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا هم فایل pdf  و هم فایل عکس را آپلود کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                if (txtCatalougeName.Text != "")
                {
                    ViewModel.tblCatalog NewCatalog = new ViewModel.tblCatalog();
                    NewCatalog.IDCatalog = Guid.NewGuid();
                    NewCatalog.Name_En = txtCatalougeName.Text.FixFarsi();
                    NewCatalog.IDRet = hfIDLegalUser.Value.StringToGuid();
                    NewCatalog.IDAttachPic = hfIDCatalogPic.Value.StringToGuid();
                    NewCatalog.IDAttachPDF = hfIDAttachCatalogFile.Value.StringToGuid();
                    NewCatalog.Sort = txtCatalogSort.Text.StringToInt();
                    NewCatalog.Status = 1;
                    bool ret = BisCatalog.AddCatalog(NewCatalog);
                    if (ret)
                    {
                        hfIDAttachCatalogFile.Value = hfIDCatalogPic.Value = "";
                        btnCatalogPic.ImageUrl = "/Admin/images/DefaultImage.png";
                        FillGrdSupplierCatalog(hfIDLegalUser.Value.StringToGuid());
                        txtCatalougeName.Text = txtCatalogSort.Text = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ثبت اطلاعات با موفقیت انجام شد</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> نام کاتالوگ را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch { }
        }

        protected void FillGrdSupplierCatalog(Guid IDSupplier)
        {
            try
            {
                ViewModel.Search SearchCatalog = new ViewModel.Search();
                SearchCatalog.Filter = " AND tblCatalog.IDRet = '" + IDSupplier + "'";
                SearchCatalog.Order = " ORDER BY tblCatalog.[Sort]";
                DataSet dsCatalog = BisCatalog.GetCatalogData(SearchCatalog);
                grdSupplierCatalog.DataSource = dsCatalog;
                grdSupplierCatalog.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}