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

using System.Web.Services;
using System.Web.Hosting;

namespace SCMCore.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.LegalUserMethod BisLegalUser = new Bis.LegalUserMethod();
        Bis.AttachInterfaceCategoryMethod BisCategory = new Bis.AttachInterfaceCategoryMethod();
        Bis.AccessoryProductMethod BisAccessoryProduct = new Bis.AccessoryProductMethod();

        Bis.PropertyMethod BisProperty = new Bis.PropertyMethod();
        Bis.AttachSiteMethod bisAttachSiteMethod = new Bis.AttachSiteMethod();
        Bis.AttachCrmInterfaceMethod bisAttachCrmInterfaceMethod = new Bis.AttachCrmInterfaceMethod();
        Bis.DetailAssignPropertyMethod BisDetailAssignProperty = new Bis.DetailAssignPropertyMethod();
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();
        Bis.DetailTechnicalPropertyMethod BisProductTechnicalProperty = new Bis.DetailTechnicalPropertyMethod();
        Bis.MadeCountryMethods BisMadeCountry = new Bis.MadeCountryMethods();
        Bis.ProductFamilyMethod BisProductFamily = new Bis.ProductFamilyMethod();
        Bis.UnitMethod BisUnit = new Bis.UnitMethod();
        Bis.RulePropertyProductMethod BisRuleProperty = new Bis.RulePropertyProductMethod();


        public string queryStringSearch = "";
        public string queryStringIDMasterProduct = "";
        public DataRow[] dr;
        DataSet dsUser = new DataSet();
        Guid IDUser;
        public string IDSupplier;
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                queryStringIDMasterProduct = Request.QueryString["IDMasterProduct"].ToString();
                if (queryStringIDMasterProduct != "")
                {
                    Session["IDSupplierForProductPage"] = "32237497-314B-4907-BC01-17A1D85127D5"; //SOL
                }
            }
            catch (Exception)
            {

               
            }

            dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();

            if (Session["IDSupplierForProductPage"] != null)
            {
                IDSupplier = Session["IDSupplierForProductPage"].ToString();
            }
            else
            {
                Response.Redirect("LegalSupplier.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multiPart/form-data");
            btnAddProduct.Attributes.Add("onclick", "javascript:return validate()");

            try
            {
                queryStringSearch = Request.QueryString["search"].ToString();
            }
            catch
            {

            }

            if (!Page.IsPostBack)
            {
                hfModeProductCategory.Value = "New";
                filltvShowProductCategory();
                fillDrpProductFamily();
                divTableProduct.Visible = false;
                divInfoProduct.Visible = false;
                divInfoProductCategory.Visible = false;
                divTableProductCategory.Visible = true;
                lblSpplierNameForProductCategory.Text = GetSupplierName(IDSupplier);
                if (queryStringSearch != "") // agar az panel kenare safhe kalamei search shavad
                {
                    searchFromAdminMaster();
                }

             

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    queryStringIDMasterProduct = Request.QueryString["IDMasterProduct"].ToString();
                    if (queryStringIDMasterProduct != "")
                    {
                        btnShowModalDefinefromQueryStringMaster.Visible = true;
                    }
                    else
                    {
                        btnShowModalDefinefromQueryStringMaster.Visible = false;
                    }
                }
                catch
                {
                    btnShowModalDefinefromQueryStringMaster.Visible = false;

                }
            }
        }

        protected void btnAddProductCategory_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                if (txtGroupName_En.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('عنوان گروه محصولات (En) را وارد کنید!');", true);
                    return;
                }

                ViewModel.tblProductCategory newProductCategory = new ViewModel.tblProductCategory();
                newProductCategory.Name_Fa = txtGroupName_Fa.Text.FixFarsi();
                newProductCategory.Name_En = txtGroupName_En.Text;
                newProductCategory.Description_Fa = txtDescriptionProductcategory_Fa.Text.FixFarsi();
                newProductCategory.Description_En = txtDescriptionProductcategory_En.Text.FixFarsi();
                newProductCategory.ParentID = hfIDParentGroup.Value.StringToGuid();
                newProductCategory.Order = txtOrder.Text.StringToInt();
                newProductCategory.MetaTags = txtMetaTagsCategory.Text.FixFarsi();
                newProductCategory.MetaDescriptions = txtMetaDescriptionsCategory.Text.FixFarsi();
                newProductCategory.ShowInSite = chkShowCategoryInSite.Checked;
                newProductCategory.Status = 1;

                switch (hfModeProductCategory.Value)
                {
                    case "New":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddProductCategory.ToString(), IDUser);
                            if (!check)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }

                            if (fuProductCategory.FileName != "")
                            {
                                string url = UploadImage(Server.MapPath("../Picture/ProductCategory/"), "Picture/ProductCategory/", fuProductCategory);
                                if (url != "")
                                {
                                    newProductCategory.PicUrl = url;

                                }
                                else return;
                            }
                            else
                            {
                                newProductCategory.PicUrl = "";
                            }
                            newProductCategory.IDProductCategory = Guid.NewGuid();
                            newProductCategory.IDSupplier = IDSupplier.StringToGuid();
                            bool ret = BisProductCategory.AddProductCategory(newProductCategory);
                            if (ret)
                            {


                                hfIDProductCategory.Value = newProductCategory.IDProductCategory.ToString();
                                filltvShowProductCategory();
                                NewFieldsProductGroup();

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
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.EditProductCategory.ToString(), IDUser);
                            if (!check)
                            {

                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }

                            if (fuProductCategory.FileName != "")
                            {
                                string url = UploadImage(Server.MapPath("../Picture/ProductCategory/"), "Picture/ProductCategory/", fuProductCategory);
                                if (url != "")
                                {
                                    newProductCategory.PicUrl = url;
                                }
                                else return;
                            }
                            else
                            {
                                newProductCategory.PicUrl = Session["OldUrlProductCategory"].ToString();
                            }
                            newProductCategory.IDProductCategory = hfIDProductCategory.Value.StringToGuid();

                            bool result = BisProductCategory.UpdateProductCategory(newProductCategory);
                            if (result)
                            {

                                if (Session["OldUrlProductCategory"] != "" && fuProductCategory.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlProductCategory"].ToString()));
                                }
                                Session.Remove("OldUrlProductCategory");
                                imgProductCategory.Visible = false;
                                filltvShowProductCategory();
                                NewFieldsProductGroup();
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

        protected void btnNewProductCategory_Click(object sender, EventArgs e)
        {
            NewFieldsProductGroup();
            divTableProduct.Visible = false;
            divInfoProduct.Visible = false;
        }

        public void NewFieldsProductGroup()
        {
            txtGroupName_Fa.Text = "";
            txtGroupName_En.Text = "";
            txtDescriptionProductcategory_Fa.Text = "";
            txtDescriptionProductcategory_En.Text = "";
            txtMetaDescriptionsCategory.Text = "";
            txtMetaTagsCategory.Text = "";
            hfIDParentGroup.Value = "";
            txtParentGroup.Text = "";
            txtParentGroup.Enabled = true;
            hfModeProductCategory.Value = "New";
            txtOrder.Text = "";
            imgProductCategory.Visible = false;
            chkShowCategoryInSite.Checked = true;
            Session.Remove("OldUrlProductCategory");
        }

        protected void btnNewGridProductCategory_Click(object sender, EventArgs e)
        {
            divTableProduct.Visible = false;
            divInfoProduct.Visible = false;
            divInfoProductCategory.Visible = true;
            divTableProductCategory.Visible = false;
            NewFieldsProductGroup();
        }

        protected void btnGridProductCategory_Click(object sender, EventArgs e)
        {
            divTableProduct.Visible = false;
            divInfoProduct.Visible = false;
            divInfoProductCategory.Visible = false;
            divTableProductCategory.Visible = true;
            Response.Redirect("Product.aspx");
        }

        public void fillGrdProduct(Guid IDProductCategory)
        {
            ViewModel.Search ProductSearch = new ViewModel.Search();
            DataSet dsProduct = new DataSet();


            if (queryStringSearch != "")
            {
                ProductSearch.Filter = "and (tblProduct.Name_Fa like N'%" + queryStringSearch + "%' or tblProduct.Name_En like N'%" + queryStringSearch + "%' )";
            }

            else
            {
                ProductSearch.Filter = " and tblProduct.IDProductCategory = '" + IDProductCategory + "'";
            }

            dsProduct = BisProduct.GetProductData(ProductSearch);
            grdProductDetails.DataSource = dsProduct;
            grdProductDetails.DataBind();
            Session["dsProduct"] = dsProduct;
            Session["dsProductIndex"] = dsProduct;

        }

        public void fillDrpProductFamily()
        {
            ViewModel.Search getProductFamily = new ViewModel.Search();
            DataSet dsProductFamily = BisProductFamily.GetProductFamilyData(getProductFamily);
            drpProductFamily.DataSource = dsProductFamily;
            drpProductFamily.DataTextField = "Name_En";
            drpProductFamily.DataValueField = "IDProductFamily";
            drpProductFamily.DataBind();
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtProduct_En.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('خطا.عنوان محصول (En) را وارد کنید!');", true);
                    return;
                }
                if (hfIDCategoryForProductInAutoComplete.Value == "" || hfIDCategoryForProductInAutoComplete.Value == Guid.Empty.ToString())
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('خطا.عنوان گروه را ار لیست انتخاب کنید!');", true);
                    return;
                }

                ViewModel.Search getProductcategory = new ViewModel.Search();
                getProductcategory.Filter = " and IDProductCategory='" + hfIDProductCategory.Value + "'";
                DataSet dsProductCategory = BisProductCategory.GetProductCategoryData(getProductcategory);

                DataSet dsUser = new DataSet();
                dsUser = (DataSet)Session["User"];
                ViewModel.tblProduct newProduct = new ViewModel.tblProduct();
                newProduct.Name_Fa = txtProduct_Fa.Text.FixFarsi();
                newProduct.Name_En = txtProduct_En.Text.FixFarsi();
                newProduct.Description_Fa = txtDescription_Fa.Text.FixFarsi();
                newProduct.Description_En = txtDescription_En.Text.FixFarsi();

                newProduct.IDSupplier = dsProductCategory.ReturnDataSetField("IDSupplier").StringToGuid();

                newProduct.IDProductFamily = drpProductFamily.SelectedValue.StringToGuid();
                newProduct.MetaTags = txtMetaTags.Text; ;
                newProduct.MetaDescriptions = txtMetaDescriptions.Text;
                newProduct.IDProductCategory = hfIDCategoryForProductInAutoComplete.Value.StringToGuid();
                newProduct.Accessory = false;
                newProduct.IDPersonel = dsUser.ReturnDataSetField("IDUser").StringToGuid();
                newProduct.Sort = txtSort.Text.StringToInt();
                newProduct.IndexDescriptionText = txtIndexDescriptionText.Text.FixFarsi();
                newProduct.Status = 1;

                switch (hfModeProduct.Value)
                {
                    case "New":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddProduct.ToString(), IDUser);
                            if (!check)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }

                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Product/"), "Picture/Product/", fulPicUrl);
                                if (url != "") newProduct.ProductUrl = url;
                                else return;
                            }
                            else
                            {
                                newProduct.ProductUrl = "";
                            }

                            if (fulIndexDescription.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/IndexDescription/"), "Picture/IndexDescription/", fulIndexDescription);
                                if (url != "") newProduct.IndexDescriptionPicUrl = url;
                                else return;
                            }
                            else
                            {
                                newProduct.IndexDescriptionPicUrl = "";
                            }

                            newProduct.IDProduct = Guid.NewGuid();
                            bool ret = BisProduct.AddProduct(newProduct);
                            if (ret)
                            {
                                hfIDProduct.Value = newProduct.IDProduct.ToString();
                                fillGrdProduct(hfIDProductCategory.Value.StringToGuid());
                                NewFieldsProduct();
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
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.EditProduct.ToString(), IDUser);
                            if (!check)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }

                            if (fulPicUrl.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/Product/"), "Picture/Product/", fulPicUrl);
                                if (url != "") newProduct.ProductUrl = url;
                                else return;
                            }
                            else
                            {
                                newProduct.ProductUrl = Session["OldUrlProduct"].ToString();
                            }


                            if (fulIndexDescription.FileName != "")
                            {
                                string url = UploadFile(Server.MapPath("../Picture/IndexDescription/"), "Picture/IndexDescription/", fulIndexDescription);
                                if (url != "") newProduct.IndexDescriptionPicUrl = url;
                                else return;
                            }
                            else
                            {
                                newProduct.IndexDescriptionPicUrl = Session["OldUrlIndexDescription"].ToString();
                            }
                            newProduct.IDProduct = hfIDProduct.Value.StringToGuid();

                            bool result = BisProduct.UpdateProduct(newProduct);
                            if (result)
                            {
                                fillGrdProduct(hfIDProductCategory.Value.StringToGuid());
                                if (Session["OldUrlProduct"] != "" && fulPicUrl.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlProduct"].ToString()));
                                }
                                Session.Remove("OldUrlProduct");
                                if (Session["OldUrlIndexDescription"] != "" && fulIndexDescription.FileName != "")
                                {
                                    File.Delete(Server.MapPath("../" + Session["OldUrlIndexDescription"].ToString()));
                                }
                                Session.Remove("OldUrlIndexDescription");

                                NewFieldsProduct();
                                imgProduct.Visible = false;
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

        protected void btnNewProduct_Click(object sender, EventArgs e)
        {
            NewFieldsProduct();
        }

        public void NewFieldsProduct()
        {
            txtProduct_Fa.Text = txtProduct_En.Text = txtDescription_Fa.Text = txtDescription_En.Text = txtMetaTags.Text = txtMetaDescriptions.Text = txtIndexDescriptionText.Text = txtCategoryForProduct.Text = "";
            imgProduct.Visible = false;
            imgIndexDescription.Visible = false;
            txtCategoryForProduct.Enabled = false;
            try
            {
                if (hfIDProductCategory.Value != "")
                {
                    hfIDCategoryForProductInAutoComplete.Value = hfIDProductCategory.Value;
                    ViewModel.Search getProductCategory = new ViewModel.Search();
                    getProductCategory.Filter = " and IDProductCategory = '" + hfIDCategoryForProductInAutoComplete.Value + "'";
                    DataSet ds = BisProductCategory.GetProductCategoryData(getProductCategory);

                    txtCategoryForProduct.Text = ds.ReturnDataSetField("ParentName_En");
                }
                else
                {
                    txtCategoryForProduct.Text = "";
                    hfIDCategoryForProductInAutoComplete.Value = "";
                }

            }
            catch
            {

            }
            hfModeProduct.Value = "New";
            Session.Remove("OldUrlProduct");
        }

        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            SearchProduct(txtSearchProduct.Text.FixFarsi());
        }

        protected void SearchProduct(string filter)
        {
            DataSet dsSearch = (DataSet)Session["dsProduct"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("Name_Fa like '%" + filter + "%' or Name_En like '%" + filter + "%'"))
            {
                dt.ImportRow(row);
            }
            grdProductDetails.DataSource = dt;  // baraye search
            grdProductDetails.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsProductIndex"] = ds;
        }

        protected void btnNewGridProduct_Click(object sender, EventArgs e)
        {

            NewFieldsProduct();
            divTableProduct.Visible = false;
            divInfoProduct.Visible = true;
            txtCategoryForProduct.Enabled = false;



        }

        protected void btnGridProduct_Click(object sender, EventArgs e)
        {
            divTableProduct.Visible = true;
            divInfoProduct.Visible = false;
            divInfoProductCategory.Visible = false;
            divTableProductCategory.Visible = false;
            fillGrdProduct(hfIDProductCategory.Value.StringToGuid());
        }

        protected void grdProductDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDProduct = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        Session.Remove("OldUrlProduct");
                        hfIDProduct.Value = IDProduct.ToString();
                        ViewModel.Search ProductSearch = new ViewModel.Search();
                        ProductSearch.Filter = " and tblProduct.IDProduct ='" + IDProduct + "'";
                        DataSet ds = BisProduct.GetProductData(ProductSearch);
                        if (!ds.Null_Ds())
                        {
                            txtProduct_Fa.Text = ds.ReturnDataSetField("Name_Fa");
                            txtProduct_En.Text = ds.ReturnDataSetField("Name_En");
                            txtDescription_Fa.Text = ds.ReturnDataSetField("Description_Fa");
                            txtDescription_En.Text = ds.ReturnDataSetField("Description_En");
                            txtSort.Text = ds.ReturnDataSetField("Sort");
                            txtMetaTags.Text = ds.ReturnDataSetField("MetaTags");
                            txtMetaDescriptions.Text = ds.ReturnDataSetField("MetaDescriptions");

                            txtCategoryForProduct.Enabled = false;
                            hfIDCategoryForProductInAutoComplete.Value = ds.ReturnDataSetField("IDProductCategory");


                            ViewModel.Search getProductCategory = new ViewModel.Search();
                            getProductCategory.Filter = " and IDProductCategory = '" + hfIDCategoryForProductInAutoComplete.Value + "'";
                            DataSet dsCategory = BisProductCategory.GetProductCategoryData(getProductCategory);
                            txtCategoryForProduct.Text = dsCategory.ReturnDataSetField("ParentName_En");
                            txtIndexDescriptionText.Text = ds.ReturnDataSetField("IndexDescriptionText");

                            drpProductFamily.SelectedValue = ds.ReturnDataSetField("IDProductFamily");

                            hfIDProductCategory.Value = ds.ReturnDataSetField("IDProductCategory");
                            Session["OldUrlProduct"] = ds.ReturnDataSetField("ProductUrl");
                            Session["OldUrlIndexDescription"] = ds.ReturnDataSetField("IndexDescriptionPicUrl");
                            if (ds.ReturnDataSetField("ProductUrl") != "")
                            {
                                imgProduct.Visible = true;
                                imgProduct.ImageUrl = "../" + ds.ReturnDataSetField("ProductUrl");
                            }
                            if (ds.ReturnDataSetField("IndexDescriptionPicUrl") != "")
                            {
                                imgIndexDescription.Visible = true;
                                imgIndexDescription.ImageUrl = "../" + ds.ReturnDataSetField("IndexDescriptionPicUrl");
                            }
                            else
                            {
                                imgIndexDescription.Visible = false;
                            }
                            DataSet dsUser = new DataSet();
                            dsUser = (DataSet)Session["User"];

                            hfModeProduct.Value = "Edit";
                            divInfoProduct.Visible = true;
                            divTableProduct.Visible = false;
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
                        bool check = SqlHelper.CheckAccess(EventName.ListofEvents.DeleteProduct.ToString(), IDUser);
                        if (!check)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }

                        ViewModel.Search getAttachSite = new ViewModel.Search();
                        getAttachSite.Filter = " and tblAttachCrmInterface.IDRet = '" + IDProduct + "'";
                        DataSet dsAttachSite = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceData_ProductSite(getAttachSite);
                        if (!dsAttachSite.Null_Ds())
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('برای این محصول فایل هایی ثبت شده است.امکان حذف موجود نیست!');", true);
                            return;
                        }

                        ViewModel.Search getDetailAssign = new ViewModel.Search();
                        getDetailAssign.Filter = " and tblDetailAssignProperty.IDRet = '" + IDProduct + "'";
                        DataSet dsDetailAssign = BisDetailAssignProperty.GetDetailAssignPropertyData(getDetailAssign);
                        if (!dsDetailAssign.Null_Ds())
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('برای این محصول ویژگی هایی انتساب داده شده است.امکان حذف موجود نیست!');", true);
                            return;
                        }

                        ViewModel.tblProduct DeleteProduct = new ViewModel.tblProduct();
                        DeleteProduct.IDProduct = IDProduct;

                        ViewModel.Search DelProductSearch = new ViewModel.Search();
                        DelProductSearch.Filter = " and IDProduct = '" + IDProduct.ToString() + "'";
                        DataSet dsProductSearch = BisProduct.GetProductData(DelProductSearch);

                        bool retDel = BisProduct.DeleteProduct(DeleteProduct);
                        if (retDel)
                        {
                            if (dsProductSearch.ReturnDataSetField("ProductUrl") != "")
                            { File.Delete(Server.MapPath("../" + dsProductSearch.ReturnDataSetField("ProductUrl"))); }

                            fillGrdProduct(hfIDProductCategory.Value.StringToGuid());

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

                case "PropertyRule":
                    try
                    {
                        hfIDProduct.Value = IDProduct.ToString();
                        ViewModel.Search ProductSearch = new ViewModel.Search();
                        ProductSearch.Filter = " and tblProduct.IDProduct ='" + IDProduct + "'";
                        DataSet dsProduct = BisProduct.GetProductData(ProductSearch);

                        string Header = GenerateParentCategory(hfIDProductCategory.Value) + " --> " + dsProduct.ReturnDataSetField("Name_En");
                        DefineDetailProduct.setHeaderOfModal(Header);
                        DefineDetailProduct.SetHfIDProduct(IDProduct.ToString());
                        DefineDetailProduct.SetIDSupplier(IDSupplier);
                        DefineDetailProduct.FillrptParentProperty(IDProduct.ToString());
                        DefineDetailProduct.FillGrdDefineDetailProduct(hfIDProduct.Value, true);
                        DefineDetailProduct.OpenModalPropertyProductCategoryEvents();
                        DefineDetailProduct.SetImageMasterProduct("/" + dsProduct.ReturnDataSetField("ProductUrl"));
                       

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

                case "AttachSite":
                    try
                    {
                        ViewModel.Search ProductSearch = new ViewModel.Search();
                        ProductSearch.Filter = " and tblProduct.IDProduct = '" + IDProduct + "'";
                        DataSet ds = BisProduct.GetProductDataQuick(ProductSearch);
                        AttachSiteProduct.SetHfIDRetAndMode(IDProduct.ToString(), "Product");
                        AttachSiteProduct.setHeaderOfModal(ds.ReturnDataSetField("ProductName_En"));
                        AttachSiteProduct.NewFiledsAttachSite();
                        AttachSiteProduct.fillGrdAttachCrmInterface(IDProduct.ToString(), "Product");
                        AttachSiteProduct.fillgrdAttachSite();
                        hfIDProduct.Value = IDProduct.ToString();
                        AttachSiteProduct.OpenModalAttachSite();
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }

                    break;

                case "DetailAssignProperty":
                    try
                    {
                        ViewModel.Search getAssign = new ViewModel.Search();
                        getAssign.Filter = " and tblDetailAssignProperty.IDRet ='" + IDProduct.ToString() + "'";
                        DataSet ds = BisDetailAssignProperty.GetDetailAssignPropertyData(getAssign);
                        if (ds.ReturnDataSetField("MaterialType") != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اجازه تغییر برای محصولات کارگاهی را ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }


                        ViewModel.Search ProductSearch = new ViewModel.Search();
                        ProductSearch.Filter = " and IDProduct ='" + IDProduct.ToString() + "'";
                        DataSet dsProduct = BisProduct.GetProductData(ProductSearch);

                        DetailAssignProperty.fillTreeDropDownProperties();
                        DetailAssignProperty.fillGrdDetailAssignProperty(IDProduct.ToString());
                        DetailAssignProperty.SetHfIDRet(IDProduct.ToString());
                        string header = " محصول " + dsProduct.ReturnDataSetField("Name_En");
                        DetailAssignProperty.setHeaderOfModal(header);
                        DetailAssignProperty.OpenModal();

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
            }

        }

        protected void grdProductDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdProductDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProductDetails.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsProductIndex"];
            grdProductDetails.DataSource = ds;
            grdProductDetails.DataBind();
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

        public string GenerateParentCategory(string ParentID)
        {
            ViewModel.Search ParentGroup = new ViewModel.Search();
            ParentGroup.Filter = " and IDProductCategory = '" + ParentID + "'";
            DataSet ds = BisProductCategory.GetProductCategoryData(ParentGroup);

            return ds.ReturnDataSetField("ParentName_En");
        }

        protected void grdProductCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }        //accessory

        public bool ShowImage(string PicUrl)
        {
            if (PicUrl == "")
                return false;
            else
                return true;

        }

        protected void searchFromAdminMaster()
        {
            try
            {

                ViewModel.Search ProductSearch = new ViewModel.Search();
                ProductSearch.Filter = "and tblProduct.Accessory='false'and (tblProduct.Name_Fa like N'%" + queryStringSearch + "%' or tblProduct.Name_En like N'%" + queryStringSearch + "%' )";
                DataSet dsProduct = BisProduct.GetProductData(ProductSearch);
                grdProductDetails.DataSource = dsProduct;
                grdProductDetails.DataBind();
                Session["dsProduct"] = dsProduct;


                divInfoProductCategory.Visible = false;
                divTableProductCategory.Visible = false;
                divTableProduct.Visible = true;


            }
            catch
            {

            }
        }

        protected void delPicProductCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OldUrlProductCategory"] == "")
                {
                    return;
                }
                if (Session["OldUrlProductCategory"] != null)
                {
                    File.Delete(Server.MapPath("../" + Session["OldUrlProductCategory"].ToString()));
                    Session["OldUrlProductCategory"] = "";
                }

                fuProductCategory = null;
                imgProductCategory.Visible = false;

                if (hfIDProductCategory.Value != "")
                {
                    ViewModel.tblProductCategory updatePicUrl = new ViewModel.tblProductCategory();
                    updatePicUrl.PicUrl = "";
                    updatePicUrl.IDProductCategory = hfIDProductCategory.Value.StringToGuid();
                    bool ret = BisProductCategory.UpdateProductCategoryPicUrl(updatePicUrl);
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

        protected void delPicProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OldUrlProduct"] == "")
                {
                    return;
                }
                if (Session["OldUrlProduct"] != null)
                {
                    File.Delete(Server.MapPath("../" + Session["OldUrlProduct"].ToString()));
                    Session["OldUrlProduct"] = "";
                }

                fulPicUrl = null;
                imgProduct.Visible = false;

                if (hfIDProduct.Value != "")
                {
                    ViewModel.tblProduct updatePicUrl = new ViewModel.tblProduct();
                    updatePicUrl.ProductUrl = "";
                    updatePicUrl.IDProduct = hfIDProduct.Value.StringToGuid();
                    bool ret = BisProduct.UpdateProductProductUrl(updatePicUrl);
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

        protected void btnShowProductCategoryInTree_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalShowProductCategoryInTree').modal('show');", true);
                filltvShowProductCategory();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        public void filltvShowProductCategory()
        {
            try
            {

                lblSupplierName.Text = GetSupplierName(IDSupplier);
                ViewModel.Search searchProductCategory = new ViewModel.Search();
                searchProductCategory.Filter = " And tblProductCategory.IDSupplier='" + IDSupplier + "'";
                searchProductCategory.Order = " Order By tblProductCategory.[Order]";
                DataSet ds = BisProductCategory.GetProductCategoryDataShowInTree(searchProductCategory);
                tvShowProductCategory.Nodes.Clear();
                TreeNode newNode = new TreeNode("گروه های تعریف شده", Guid.Empty.ToString());
                tvShowProductCategory.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDProductCategory", tvShowProductCategory);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }

        protected string GetSupplierName(string IDSupplier)
        {
            ViewModel.Search getSupplier = new ViewModel.Search();
            getSupplier.Filter = " and tblLegalUser.IDUser = '" + IDSupplier + "'";
            DataSet dsSupplier = BisLegalUser.GetSupplierData(getSupplier);
            return dsSupplier.ReturnDataSetField("Name_Fa");
        }

        private void BindTree(DataSet ds, TreeNode parentNode, string Key, TreeView tv)
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
                TreeNode newNode = new TreeNode("<div dir=\"ltr\">" + dr["Name_En"].ToString() + "</div>", dr[Key].ToString());
                if (parentNode == null)
                {
                    tv.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(ds, newNode, Key, tv);
            }
        }

        protected void tvShowProductCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                ViewModel.Search ProductCategorySearch = new ViewModel.Search();
                ProductCategorySearch.Filter = " and IDProductCategory ='" + tvShowProductCategory.SelectedNode.Value + "'";
                DataSet ds = BisProductCategory.GetProductCategoryData(ProductCategorySearch);
                lblProductCategoryNameInHeader.Text = ds.ReturnDataSetField("ParentName_En");
                hfIDProductCategory.Value = tvShowProductCategory.SelectedNode.Value;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalShowSelectedNodInProductCategory').modal('show');$('#ModalShowSelectedNodInProductCategory').css('overflow','hidden')", true);
                tvShowProductCategory.SelectedNode.Selected = false;


            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }

        }

        protected void btnEditProductCategoryInTree_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("OldUrlProductCategory");

                ViewModel.Search ProductCategorySearch = new ViewModel.Search();
                ProductCategorySearch.Filter = " and IDProductCategory ='" + hfIDProductCategory.Value + "'";

                DataSet ds = BisProductCategory.GetProductCategoryData(ProductCategorySearch);
                if (!ds.Null_Ds())
                {

                    fillGrdProduct(hfIDProductCategory.Value.StringToGuid());
                    txtGroupName_Fa.Text = ds.ReturnDataSetField("Name_Fa");
                    txtGroupName_En.Text = ds.ReturnDataSetField("Name_En");

                    txtDescriptionProductcategory_Fa.Text = ds.ReturnDataSetField("Description_Fa");
                    txtDescriptionProductcategory_En.Text = ds.ReturnDataSetField("Description_En");
                    txtMetaDescriptionsCategory.Text = ds.ReturnDataSetField("MetaDescriptions");
                    txtMetaTagsCategory.Text = ds.ReturnDataSetField("MetaTags");
                    chkShowCategoryInSite.Checked = ds.ReturnDataSetField("ShowInSite").StringToBool();
                    hfIDParentGroup.Value = ds.ReturnDataSetField("ParentID");
                    txtParentGroup.Text = ds.ReturnDataSetField("ParentName_En");
                    txtParentGroup.Enabled = false;


                    hfModeProductCategory.Value = "Edit";
                    divTableProduct.Visible = false;
                    divInfoProduct.Visible = false;
                    divInfoProductCategory.Visible = true;
                    divTableProductCategory.Visible = false;

                    if (ds.ReturnDataSetField("PicUrl") != "")
                    {
                        imgProductCategory.Visible = true;
                        imgProductCategory.ImageUrl = "../" + ds.ReturnDataSetField("PicUrl");
                    }
                    Session["OldUrlProductCategory"] = ds.ReturnDataSetField("PicUrl");
                    txtOrder.Text = ds.ReturnDataSetField("Order");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalShowSelectedNodInProductCategory').modal('hide');", true);

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
        }

        protected void btnAddProductInTree_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.Search ProductCategorySearch = new ViewModel.Search();
                ProductCategorySearch.Filter = " and IDProductCategory ='" + hfIDProductCategory.Value + "'";
                DataSet ds = BisProductCategory.GetProductCategoryData(ProductCategorySearch);
                lblProductCategoryNameInProduct.Text = GenerateParentCategory(hfIDProductCategory.Value);
                if (!ds.Null_Ds())
                {
                    fillGrdProduct(hfIDProductCategory.Value.StringToGuid());
                    hfModeProductCategory.Value = "AddProduct";
                    divTableProduct.Visible = true;
                    divInfoProduct.Visible = false;
                    divInfoProductCategory.Visible = false;
                    divTableProductCategory.Visible = false;
                    imgProductCategory.Visible = false;

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalShowSelectedNodInProductCategory').modal('hide');", true);

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
        }

        protected void btnDelProductCategoryInTree_Click(object sender, EventArgs e)
        {
            try
            {
                bool check = SqlHelper.CheckAccess(EventName.ListofEvents.DeleteProductCategory.ToString(), IDUser);
                if (!check)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }

                ViewModel.Search ProductSearch = new ViewModel.Search();
                ProductSearch.Filter = " and tblProduct.IDProductCategory ='" + hfIDProductCategory.Value + "'";
                DataSet dsProduct = BisProduct.GetProductData(ProductSearch);
                if (!dsProduct.Null_Ds())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('برای این گروه محصولاتی ثبت شده است.امکان حذف موجود نیست!');", true);
                    return;
                }

                ViewModel.Search ProductCategorySearch = new ViewModel.Search();
                ProductCategorySearch.Filter = " and ParentID ='" + hfIDProductCategory.Value + "'";
                DataSet dsProductCategoryParent = BisProductCategory.GetProductCategoryData(ProductCategorySearch);
                if (!dsProductCategoryParent.Null_Ds())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('ابتدا زیر گروه های مرتبط با این گروه را پاک کنید!');", true);
                    return;
                }


                ViewModel.Search DelProductCategory = new ViewModel.Search();
                DelProductCategory.Filter = " and IDProductCategory = '" + hfIDProductCategory.Value + "'";
                DataSet dsCategorySearch = BisProductCategory.GetProductCategoryData(DelProductCategory);

                ViewModel.tblProductCategory DeleteProductCategory = new ViewModel.tblProductCategory();
                DeleteProductCategory.IDProductCategory = hfIDProductCategory.Value.StringToGuid();
                DeleteProductCategory.ParentID = null;

                bool retDel = BisProductCategory.DeleteProductCategory(DeleteProductCategory);
                if (retDel)
                {
                    if (dsCategorySearch.ReturnDataSetField("PicUrl") != "")
                    { File.Delete(Server.MapPath("../" + dsCategorySearch.ReturnDataSetField("PicUrl"))); }
                    filltvShowProductCategory();
                    NewFieldsProductGroup();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                }
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr2", "$('#ModalShowSelectedNodInProductCategory').modal('hide');", true);


            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }
        }

        protected void btnReturnSupplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("LegalSupplier.aspx");
        }

        protected void btnDelIndexDescriptionPic_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["OldUrlIndexDescription"] == "")
                {
                    return;
                }
                if (Session["OldUrlIndexDescription"] != null)
                {
                    File.Delete(Server.MapPath("../" + Session["OldUrlIndexDescription"].ToString()));
                    Session["OldUrlIndexDescription"] = "";
                }

                fulIndexDescription = null;
                imgIndexDescription.Visible = false;

                if (hfIDProduct.Value != "")
                {
                    ViewModel.tblProduct updatePicUrl = new ViewModel.tblProduct();
                    updatePicUrl.IndexDescriptionPicUrl = "";
                    updatePicUrl.IDProduct = hfIDProduct.Value.StringToGuid();
                    bool ret = BisProduct.UpdateProduct(updatePicUrl);
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

  

        protected void btnShowModalDefinefromQueryStringMaster_Click(object sender, EventArgs e)
        {
            try
            {
                queryStringIDMasterProduct = Request.QueryString["IDMasterProduct"].ToString();
                if (queryStringIDMasterProduct != "")
                {

                    hfIDProduct.Value = queryStringIDMasterProduct;
                    ViewModel.Search ProductSearch = new ViewModel.Search();
                    ProductSearch.Filter = " and tblProduct.IDProduct ='" + queryStringIDMasterProduct + "'";
                    DataSet dsProduct = BisProduct.GetProductData(ProductSearch);
                    string Header = GenerateParentCategory(dsProduct.ReturnDataSetField("IDProductCategory")) + " --> " + dsProduct.ReturnDataSetField("Name_En"); 
                    DefineDetailProduct.setHeaderOfModal(Header);
                    DefineDetailProduct.SetHfIDProduct(queryStringIDMasterProduct);
                    DefineDetailProduct.SetIDSupplier(IDSupplier);
                    DefineDetailProduct.FillrptParentProperty(queryStringIDMasterProduct);
                    DefineDetailProduct.FillGrdDefineDetailProduct(hfIDProduct.Value, true);
                    DefineDetailProduct.OpenModalPropertyProductCategoryEvents();
                    DefineDetailProduct.SetImageMasterProduct("/" + dsProduct.ReturnDataSetField("ProductUrl"));


                }
            }
            catch (Exception)
            {

            }
        }
    }
}