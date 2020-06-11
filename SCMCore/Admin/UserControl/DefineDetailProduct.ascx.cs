using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Admin.UserControl
{
    public partial class DefineDetailProduct : System.Web.UI.UserControl
    {
        Bis.RulePropertyProductMethod BisRuleProperty = new Bis.RulePropertyProductMethod();
        Bis.DetailTechnicalPropertyMethod BisProductTechnicalProperty = new Bis.DetailTechnicalPropertyMethod();
        Bis.DetailAssignPropertyMethod BisDetailAssignProperty = new Bis.DetailAssignPropertyMethod();
        Bis.PropertyMethod BisProperty = new Bis.PropertyMethod();
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.UnitMethod BisUnit = new Bis.UnitMethod();
        Bis.MadeCountryMethods BisMadeCountry = new Bis.MadeCountryMethods();
        Bis.AttachCrmInterfaceMethod bisAttachCrmInterfaceMethod = new Bis.AttachCrmInterfaceMethod();
        Bis.RelatedDefineDetailProductMethod BisRelatedDefineDetailProduct = new Bis.RelatedDefineDetailProductMethod();
        Bis.AccessoryProductMethod BisAccessoryProduct = new Bis.AccessoryProductMethod();
        Bis.ProductDefineDetailProductMethod BisProductDefineDetailProduct = new Bis.ProductDefineDetailProductMethod();
        Bis.AssignPropertyRetMethod BisAssignPropertyRet = new Bis.AssignPropertyRetMethod();

        DataSet dsUser = new DataSet();
        Guid IDUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDrpUnit();
                fillDrpMadeCountry();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["dsDefineDetailProduct"] != null)
            {
                DataSet ds = (DataSet)Session["dsDefineDetailProduct"];
                lblGrdDefineRowsCount.Text = ds.Tables[0].Rows.Count.ToString();
            }
            if (hfIDDefineDetailProduct.Value != "")
            {
                fillGrdProductDefineDetailProduct();

            }
        }

        protected void grdDefineDetailProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDefineDetailProduct.PageIndex = e.NewPageIndex;

            DataSet ds = (DataSet)Session["dsDefineDetailProductIndex"];
            grdDefineDetailProduct.DataSource = ds;
            grdDefineDetailProduct.DataBind();
        }

        protected void grdDefineDetailProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDDefineDetailProduct = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {
                        bool check = SqlHelper.CheckAccess(EventName.ListofEvents.DeleteDefineDetailProduct.ToString(), IDUser);
                        if (!check)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }

                        ViewModel.Search getAttachSite = new ViewModel.Search();
                        getAttachSite.Filter = " and tblAttachCrmInterface.IDRet = '" + IDDefineDetailProduct + "'";
                        DataSet dsAttachSite = bisAttachCrmInterfaceMethod.GetAttachCrmInterfaceData_DefineDetailProductSite(getAttachSite);
                        if (!dsAttachSite.Null_Ds())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('برای این ترکیب فایل هایی ثبت شده است.امکان حذف موجود نیست!');", true);
                            return;
                        }

                        ViewModel.Search getDetailTechnicalProperty = new ViewModel.Search();
                        getDetailTechnicalProperty.Filter = " and tblDetailTechnicalProperty.IDRet = '" + IDDefineDetailProduct + "'";
                        DataSet dsDetailTechnicalProperty = BisProductTechnicalProperty.GetDetailTechnicalPropertyData(getDetailTechnicalProperty);
                        if (!dsDetailTechnicalProperty.Null_Ds())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('برای این ترکیب ویژگی های فنی اختصاص داده شده است.امکان حذف موجود نیست!');", true);
                            return;
                        }

                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);

                        ViewModel.tblDefineDetailProduct deleteDefine = new ViewModel.tblDefineDetailProduct();
                        deleteDefine.IDDefineDetailProduct = IDDefineDetailProduct;
                        bool ret = BisDefineDetailProduct.DeleteDefineDetailProductOneRow(deleteDefine);
                        if (ret)
                        {
                            if (dsDefineDetailProduct.ReturnDataSetField("PicUrl") != "")
                            { File.Delete(Server.MapPath(@"..\" + dsDefineDetailProduct.ReturnDataSetField("PicUrl"))); }
                            if (dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionPicUrl") != "")
                            { File.Delete(Server.MapPath(@"..\" + dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionPicUrl"))); }
                            FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت حذف شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
                case "Edit":
                    try
                    {
                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);
                        if (!dsDefineDetailProduct.Null_Ds())
                        {
                            FileUploadForDefineDetail.ClearFileUpload();
                            txtPartNumber.Text = dsDefineDetailProduct.ReturnDataSetField("PartNumber");
                            txtShortTechnicalDescription_Fa.Text = dsDefineDetailProduct.ReturnDataSetField("ShortTechnicalDescription_Fa");
                            txtShortTechnicalDescription.Text = dsDefineDetailProduct.ReturnDataSetField("ShortTechnicalDescription");
                            txtTechnicalDescription_Fa.Text = dsDefineDetailProduct.ReturnDataSetField("TechnicalDescription_Fa");
                            txtTechnicalDescription.Text = dsDefineDetailProduct.ReturnDataSetField("TechnicalDescription");
                            txtDescriptionInSite_En.Text = dsDefineDetailProduct.ReturnDataSetField("DescriptionInSite_En");
                            txtDescriptionInSite_Fa.Text = dsDefineDetailProduct.ReturnDataSetField("DescriptionInSite_Fa");
                            txtMetaTag.Text = dsDefineDetailProduct.ReturnDataSetField("MetaTag");
                            txtMetaDescription.Text = dsDefineDetailProduct.ReturnDataSetField("MetaDescription");
                            drpUnit.SelectedValue = dsDefineDetailProduct.ReturnDataSetField("IDUnit");
                            drpMadeCountry.SelectedValue = dsDefineDetailProduct.ReturnDataSetField("IDMadeCountry");
                            chkShowInSite.Checked = dsDefineDetailProduct.ReturnDataSetField("ShowInSite").StringToBool();
                            chkSpecialDefine.Checked = dsDefineDetailProduct.ReturnDataSetField("SpecialDefine").StringToBool();
                            hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();
                            txtIndexDescriptionText.Text = dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionText");
                            txtSort.Text = dsDefineDetailProduct.ReturnDataSetField("Sort");
                            lblDefineDetailCombinationDescription.Text = "ویرایش ترکیب برای " + dsDefineDetailProduct.ReturnDataSetField("ProductName_En") + " مدل " + dsDefineDetailProduct.ReturnDataSetField("CombinationDescription");
                            if (dsDefineDetailProduct.ReturnDataSetField("PicUrl") != "")
                            {
                                hfDefineDetailPicUrl.Value = dsDefineDetailProduct.ReturnDataSetField("PicUrl");
                                DivDeleteOldPic.Visible = true;
                                imgOldDefineDetailPic.ImageUrl = @"\" + dsDefineDetailProduct.ReturnDataSetField("PicUrl");
                            }
                            if (dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionPicUrl") != "")
                            {
                                hfIndexDescriptionPicUrl.Value = dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionPicUrl");
                                DivIndexDescriptionPic.Visible = true;
                                imgIndexDescription.ImageUrl = @"\" + dsDefineDetailProduct.ReturnDataSetField("IndexDescriptionPicUrl");
                            }
                            else
                            {
                                DivIndexDescriptionPic.Visible = false;
                            }
                            hfModeDefineDetailProduct.Value = "Edit";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalDefineDetailPartNumber').modal('show');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }


                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }

                    break;
                case "EditRules":
                    try
                    {
                        ViewModel.tblDefineDetailProduct getRules = new ViewModel.tblDefineDetailProduct();
                        getRules.IDDefineDetailProduct = IDDefineDetailProduct;
                        DataSet dsRules = BisDefineDetailProduct.GetDataSetPropertyNameValueDataByIDDefineDetailProduct(getRules);
                        if (!dsRules.Null_Ds())
                        {
                            hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();
                            btnSaveRuleChanges.Visible = true;
                            btnAddRulePropertyAllRow.Visible = false;
                            foreach (DataRow dr in dsRules.Tables[0].Rows)
                            {
                                foreach (RepeaterItem ri in rptParentProperty.Items)
                                {
                                    string hfIDParentProperty = ((HiddenField)ri.FindControl("hfIDParentProperty")).Value;
                                    if (hfIDParentProperty == dr["ParentIDProperty"].ToString())
                                    {
                                        ((DropDownList)ri.FindControl("drpChildSubProperty")).SelectedValue = dr["IDProperty"].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }


                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }

                    break;
                case "AttachSite":
                    try
                    {

                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);

                        AttachSiteDefines.SetHfIDRetAndMode(IDDefineDetailProduct.ToString(), "DefineDetailProduct");
                        string header = dsDefineDetailProduct.ReturnDataSetField("ProductName_En") + "  مدل " + dsDefineDetailProduct.ReturnDataSetField("CombinationDescription");
                        AttachSiteDefines.setHeaderOfModal(header);
                        AttachSiteDefines.NewFiledsAttachSite();
                        AttachSiteDefines.fillGrdAttachCrmInterface(IDDefineDetailProduct.ToString(), "DefineDetailProduct");
                        AttachSiteDefines.fillgrdAttachSite();

                        hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();

                        AttachSiteDefines.OpenModalAttachSite();
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }

                    break;
                case "TechnicalProperty":
                    try
                    {


                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);

                        TechnicalProperty.SetHfIDRet(IDDefineDetailProduct.ToString());
                        TechnicalProperty.newFieldsDetailTechnicalProperty();
                        TechnicalProperty.fillGrdDetailTechnicalProperty(IDDefineDetailProduct);
                        string header = dsDefineDetailProduct.ReturnDataSetField("ProductName_En") + "  مدل " + dsDefineDetailProduct.ReturnDataSetField("CombinationDescription");
                        TechnicalProperty.setHeaderOfModal(header);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalTechnicalProperty').modal('show');", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalTechnicalProperty').modal('show');", true);
                    break;
                case "UnderSpot":
                    try
                    {
                        ViewModel.tblDefineDetailProduct UpdateDefineDetailProduct = new ViewModel.tblDefineDetailProduct();
                        UpdateDefineDetailProduct.IDDefineDetailProduct = IDDefineDetailProduct;
                        bool retUpdate = BisDefineDetailProduct.UpdateUnderSpot(UpdateDefineDetailProduct);
                        if (retUpdate)
                        {
                            FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت Under Spot');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
                case "Relation":
                    try
                    {
                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);

                        hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();
                        FillSelectedTreeProductDefine();
                        TreeProduct.InitialDataSource();
                        FillGrdRelatedDefineDetailProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                        lblBaseRelatedDefineName.Text = dsDefineDetailProduct.ReturnDataSetField("PartNumber");
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ModalRelations", "$('#ModalRelatedDefineDetailProduct').modal('show');", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
                case "Accessory":
                    try
                    {
                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisDefineDetailProduct.GetDefineDetailProductData(getDefineDetailProduct);
                        lblBaseAccessoryDefineName.Text = dsDefineDetailProduct.ReturnDataSetField("PartNumber");


                        hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();
                        FillSelectedTreeAccessoryProduct();
                        TreeAccessory.InitialDataSource();
                        fillGrdAccessoryProduct(hfIDDefineDetailProduct.Value.StringToGuid());


                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ModalRelations", "$('#ModalAccessory').modal('show');", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
                case "AssignMasterProduct":
                    try
                    {
                        hfIDDefineDetailProduct.Value = IDDefineDetailProduct.ToString();

                        ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                        getDefineDetailProduct.Filter = " and tblProductDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct.ToString() + "'";
                        DataSet dsDefineDetailProduct = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getDefineDetailProduct);
                        lblDefineInModalAssignMasterProduct.Text = dsDefineDetailProduct.ReturnDataSetField("PartNumber");
                        TreeCategoryProduct.SetIDSupplier(hfIDSupplier.Value);
                        TreeCategoryProduct.SetIDDefineDetailProduct(IDDefineDetailProduct.ToString());
                        TreeCategoryProduct.InitialDataSource();
                        fillGrdProductDefineDetailProduct();

                        //FillSelectedTreeAccessoryProduct();
                        //TreeAccessory.InitialDataSource();
                        //fillGrdAccessoryProduct(hfIDDefineDetailProduct.Value.StringToGuid());


                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ModalRelations", "$('#ModalAssignMasterProduct').modal('show');", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;

            }
        }

        protected void grdDefineDetailProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdDefineDetailProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdDefineDetailProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Label)e.Row.FindControl("lblNo")).Text = ((grdDefineDetailProduct.PageIndex * 10) + e.Row.RowIndex + 1).ToString();

            }
        }

        public void FillGrdDefineDetailProduct(string IDRet, bool Product)
        {
            hfCheckProduct.Value = Product.ToString();
            ViewModel.Search getProductDefineDetailProduct = new ViewModel.Search();
            getProductDefineDetailProduct.Filter = " and tblProductDefineDetailProduct.IDRet = '" + IDRet + "'";
            DataSet dsProductDefineDetailProduct = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getProductDefineDetailProduct);
            grdDefineDetailProduct.DataSource = dsProductDefineDetailProduct;
            grdDefineDetailProduct.DataBind();

            Session["dsDefineDetailProduct"] = dsProductDefineDetailProduct;
            Session["dsDefineDetailProductIndex"] = dsProductDefineDetailProduct;
        }

        protected void rptParentProperty_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                string IDParentProperty = ((HiddenField)e.Item.FindControl("hfIDParentProperty")).Value;
                DropDownList drpChildSubProperty = ((DropDownList)e.Item.FindControl("drpChildSubProperty"));
                DataSet dsChildSubProperty = FillrblChildSubProperty(IDParentProperty.StringToGuid());

                drpChildSubProperty.DataValueField = "IDProperty";
                drpChildSubProperty.DataTextField = "Name_En";
                drpChildSubProperty.DataSource = dsChildSubProperty;
                drpChildSubProperty.DataBind();

                ViewModel.tblAssignPropertyRet getAssignPropertyRet = new ViewModel.tblAssignPropertyRet();
                getAssignPropertyRet.IDRet = hfIDProduct.Value.StringToGuid();
                DataSet dsAssignPropertyRet = BisAssignPropertyRet.GetAssignPropertyRetDataByIDProduct(getAssignPropertyRet);

                foreach (DataRow dr in dsAssignPropertyRet.Tables[0].Rows)
                {
                    foreach (DataRow dr2 in dsChildSubProperty.Tables[0].Rows)
                    {
                        if (dr["IDProperty"].ToString() == dr2["IDProperty"].ToString())
                        {
                            drpChildSubProperty.SelectedValue = dr["IDProperty"].ToString();
                            drpChildSubProperty.Enabled = false;
                            break;
                        }
                    }

                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }


        }

        public DataSet FillrblChildSubProperty(Guid IDParentProperty)
        {
            ViewModel.Search SearchChildSubProperty = new ViewModel.Search();
            SearchChildSubProperty.Filter = " and tblProperty.IDParent = '" + IDParentProperty + "'";
            SearchChildSubProperty.Order = " order by tblProperty.Name_En ";
            DataSet dsChildSubProperty = BisProperty.GetQuickData(SearchChildSubProperty);
            return dsChildSubProperty;
        }

        public void FillrptParentProperty(string IDRet)
        {
            ViewModel.Search SearchAsignProperty = new ViewModel.Search();
            SearchAsignProperty.Filter = " and tblDetailAssignProperty.IDRet = '" + IDRet + "'";
            SearchAsignProperty.Order = " ORDER BY tblDetailAssignProperty.Sort";
            DataSet dsAsignProperty = BisDetailAssignProperty.GetDetailAssignPropertyData(SearchAsignProperty);
            rptParentProperty.DataSource = dsAsignProperty;
            rptParentProperty.DataBind();
        }

        protected void btnAddRulePropertyAllRow_Click(object sender, EventArgs e)
        {
            bool Error = true;
            string strCombinationDescription = "";
            string strIDPropertyList = "";
            string strGenerateCode = "";
            foreach (RepeaterItem ri in rptParentProperty.Items)
            {
                DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));
                if (drp.SelectedValue == "0")
                {
                    Error = true;
                    break;
                }
                else
                {
                    if (strCombinationDescription != "")
                    {
                        strCombinationDescription += ", ";
                    }
                    strCombinationDescription += drp.SelectedItem.Text;

                    //list id property haie select shode
                    if (strIDPropertyList != "")
                    {
                        strIDPropertyList += ",";
                    }
                    strIDPropertyList += drp.SelectedValue;


                    ViewModel.Search getValueproperty = new ViewModel.Search();
                    getValueproperty.Filter = " and tblProperty.IDProperty = '" + drp.SelectedValue + "'";
                    DataSet dsProperty = BisProperty.GetPropertyData(getValueproperty);
                    if (strGenerateCode != "")
                    {
                        strGenerateCode += "-";
                    }
                    strGenerateCode += dsProperty.ReturnDataSetField("Code");

                    Error = false;
                }
            }
            if (Error)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا از هر آیتم یکی را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
            else
            {
                ViewModel.Search getDefine = new ViewModel.Search();
                getDefine.Filter = " and tblProductDefineDetailProduct.IDRet='" + hfIDProduct.Value + "' and tblDefineDetailProduct.GeneratedCode='" + strGenerateCode + "'";
                DataSet dsSearch = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getDefine);
                if (dsSearch.Null_Ds())
                {
                    ViewModel.Search getProduct = new ViewModel.Search();
                    getProduct.Filter = " and tblProduct.IDProduct='" + hfIDProduct.Value + "'";
                    DataSet dsProduct = BisProduct.GetProductData(getProduct);

                    hfCombinationDescription.Value = strCombinationDescription;
                    hfIDPropertyList.Value = strIDPropertyList;
                    hfGenrateCode.Value = strGenerateCode;
                    lblDefineDetailCombinationDescription.Text = dsProduct.ReturnDataSetField("Name_En") + " مدل " + strCombinationDescription;
                    fillDrpUnit();
                    fillDrpMadeCountry();
                    newFiledsInDefine();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalDefineDetailPartNumber').modal('show');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این ترکیب قبلا ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                }


            }
        }

        protected void newFiledsInDefine()
        {
            txtPartNumber.Text = txtTechnicalDescription.Text = txtTechnicalDescription_Fa.Text = txtShortTechnicalDescription.Text = txtShortTechnicalDescription_Fa.Text = txtDescriptionInSite_En.Text = txtDescriptionInSite_Fa.Text = txtMetaTag.Text = txtMetaDescription.Text = txtIndexDescriptionText.Text = "";
            chkSpecialDefine.Checked = false;
            drpUnit.SelectedIndex = 0;
            drpMadeCountry.SelectedIndex = 0;
            FileUploadForDefineDetail.ClearFileUpload();
            hfModeDefineDetailProduct.Value = "New";
        }

        protected void fillDrpUnit()
        {
            ViewModel.Search getUnit = new ViewModel.Search();
            DataSet dsUnit = BisUnit.GetUnitData(getUnit);
            drpUnit.DataSource = dsUnit;
            drpUnit.DataTextField = "Name_Fa";
            drpUnit.DataValueField = "IDUnit";
            drpUnit.DataBind();
        }

        protected void fillDrpMadeCountry()
        {
            ViewModel.Search getMadeCountry = new ViewModel.Search();
            DataSet dsMadeCountry = BisMadeCountry.GetMadeCountryData(getMadeCountry);
            drpMadeCountry.DataSource = dsMadeCountry;
            drpMadeCountry.DataTextField = "Sign";
            drpMadeCountry.DataValueField = "IDMadeCountry";
            drpMadeCountry.DataBind();
        }

        protected void btnAddRulePropertyOneRowByPartNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTechnicalDescription.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> توضیحات فنی را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                if (txtPartNumber.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> Part Number را وارد کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }


                ViewModel.tblDefineDetailProduct AddDefine = new ViewModel.tblDefineDetailProduct();
                AddDefine.IDUser = IDUser;
                AddDefine.IDUnit = drpUnit.SelectedValue.StringToGuid();
                AddDefine.IDMadeCountry = drpMadeCountry.SelectedValue.StringToGuid();
                AddDefine.PartNumber = txtPartNumber.Text;
                AddDefine.TechnicalDescription_Fa = txtTechnicalDescription_Fa.Text;
                AddDefine.TechnicalDescription = txtTechnicalDescription.Text;
                AddDefine.ShortTechnicalDescription = txtShortTechnicalDescription.Text;
                AddDefine.ShortTechnicalDescription_Fa = txtShortTechnicalDescription_Fa.Text;
                AddDefine.DescriptionInSite_En = txtDescriptionInSite_En.Text;
                AddDefine.DescriptionInSite_Fa = txtDescriptionInSite_Fa.Text;
                AddDefine.MetaTag = txtMetaTag.Text;
                AddDefine.MetaDescription = txtMetaDescription.Text;
                AddDefine.ShowInSite = chkShowInSite.Checked;
                AddDefine.SpecialDefine = chkSpecialDefine.Checked;
                AddDefine.IndexDescriptionText = txtIndexDescriptionText.Text.FixFarsi();
                AddDefine.CreateDate = DateTime.Now;
                AddDefine.Sort = txtSort.Text.StringToInt();


                switch (hfModeDefineDetailProduct.Value)
                {
                    case "New":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddDefineDetailProduct.ToString(), IDUser);
                            if (!check)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }
                            ViewModel.Search SearchDefineDetail = new ViewModel.Search();
                            SearchDefineDetail.Filter = " AND tblDefineDetailProduct.PartNumber='" + txtPartNumber.Text + "' ";
                            DataSet dsCheckDefineDetail = BisDefineDetailProduct.GetDefineDetailProductData(SearchDefineDetail);
                            if (dsCheckDefineDetail.Null_Ds())
                            {
                                AddDefine.IDProduct = hfIDProduct.Value.StringToGuid();
                                AddDefine.CombinationDescription = hfCombinationDescription.Value;
                                AddDefine.GeneratedCode = hfGenrateCode.Value;
                                AddDefine.IDPropertyList = hfIDPropertyList.Value;
                                AddDefine.IDDefineDetailProduct = Guid.NewGuid();
                                AddDefine.PicUrl = FileUploadForDefineDetail.MoveFile(@"..\Picture\DefineDetailProduct");
                                AddDefine.IndexDescriptionPicUrl = fulIndexDescription.MoveFile(@"..\Picture\IndexDescription");
                                int ret = BisDefineDetailProduct.AddDefineDetailProductSelectCases(AddDefine);
                                if (ret > 0)
                                {
                                    FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalDefineDetailPartNumber').modal('hide');", true);
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ترکیب مورد نظر ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});$('#ModalDefineDetailPartNumber').modal('hide');", true);

                                }
                                else
                                {
                                    try { File.Delete(Server.MapPath(@"\" + AddDefine.PicUrl)); }
                                    catch { }
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>این PartNumber قبلا ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErorDB", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }
                        break;
                    case "Edit":
                        bool check2 = SqlHelper.CheckAccess(EventName.ListofEvents.EditDefineDetailProduct.ToString(), IDUser);
                        if (!check2)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }
                        ViewModel.Search SearchDefineDetailInEdit = new ViewModel.Search();
                        SearchDefineDetailInEdit.Filter = " And tblDefineDetailProduct.IDDefineDetailProduct <> '" + hfIDDefineDetailProduct.Value + "'  AND tblDefineDetailProduct.PartNumber='" + txtPartNumber.Text + "'";
                        DataSet dsCheckDefineDetailEdit = BisDefineDetailProduct.GetDefineDetailProductData(SearchDefineDetailInEdit);
                        if (dsCheckDefineDetailEdit.Null_Ds())
                        {
                            ViewModel.Search SearchOldDefine = new ViewModel.Search();
                            SearchOldDefine.Filter = " And tblDefineDetailProduct.IDDefineDetailProduct = '" + hfIDDefineDetailProduct.Value + "'";
                            DataSet dsOldDefine = BisDefineDetailProduct.GetDefineDetailProductData(SearchOldDefine);
                            string UrlMaster = FileUploadForDefineDetail.MoveFile(@"..\Picture\DefineDetailProduct");
                            if (UrlMaster != "")
                            {
                                ViewModel.tblDefineDetailProduct UpdateMasterPic = new ViewModel.tblDefineDetailProduct();
                                UpdateMasterPic.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                                UpdateMasterPic.PicUrl = "";
                                DeleteOldPic(UpdateMasterPic, dsOldDefine.ReturnDataSetField("PicUrl"));
                                AddDefine.PicUrl = UrlMaster;
                            }
                            else
                            {

                                AddDefine.PicUrl = dsOldDefine.ReturnDataSetField("PicUrl");
                            }

                            string UrlIndexDescription = fulIndexDescription.MoveFile(@"..\Picture\IndexDescription");
                            if (UrlIndexDescription != "")
                            {
                                ViewModel.tblDefineDetailProduct UpdateIndexDescriptionPicUrl = new ViewModel.tblDefineDetailProduct();
                                UpdateIndexDescriptionPicUrl.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                                UpdateIndexDescriptionPicUrl.IndexDescriptionPicUrl = "";
                                DeleteOldPic(UpdateIndexDescriptionPicUrl, dsOldDefine.ReturnDataSetField("IndexDescriptionPicUrl"));
                                AddDefine.IndexDescriptionPicUrl = UrlIndexDescription;
                            }
                            else
                            {
                                AddDefine.IndexDescriptionPicUrl = dsOldDefine.ReturnDataSetField("IndexDescriptionPicUrl");
                            }

                            AddDefine.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                            int ret = BisDefineDetailProduct.UpdateDefineDetailProduct(AddDefine);
                            if (ret > 0)
                            {
                                if (hfIDProduct.Value != "")
                                {
                                    FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                                }
                                else
                                {
                                    FillGrdDefineDetailProduct_All();
                                }
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#ModalDefineDetailPartNumber').modal('hide');", true);
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ترکیب مورد نظر ویرایش شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});$('#ModalDefineDetailPartNumber').modal('hide');", true);
                            }
                            else
                            {
                                try { File.Delete(Server.MapPath(@"\" + AddDefine.PicUrl)); }
                                catch { }
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>این PartNumber قبلا ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }
                        break;

                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }

        }

        protected void btnSearchRuleProperty_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem ri in rptParentProperty.Items)
                {
                    DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));
                    if (drp.SelectedValue == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا از هر آیتم یکی را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        return;
                    }
                }
                DataSet ds = SearchRuleProperty();
                if (ds.Null_Ds())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> آیتمی یافت نشد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                else
                {
                    grdDefineDetailProduct.DataSource = ds;
                    grdDefineDetailProduct.DataBind();
                    Session["dsDefineDetailProduct"] = ds;
                    Session["dsDefineDetailProductIndex"] = ds;
                }

            }
            catch
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void btnNewRuleProperty_Click(object sender, EventArgs e)
        {
            newRulePropertyFields();
        }

        public void newRulePropertyFields()
        {
            foreach (RepeaterItem ri in rptParentProperty.Items)
            {
                DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));
                drp.SelectedValue = "0";
            }
            FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
            btnSaveRuleChanges.Visible = false;
            btnAddRulePropertyAllRow.Visible = true;
        }

        protected DataSet SearchRuleProperty()
        {
            string GeneratedCode = "";
            foreach (RepeaterItem ri in rptParentProperty.Items)
            {
                DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));

                ViewModel.Search getProperty = new ViewModel.Search();
                getProperty.Filter = " and tblProperty.IDProperty = '" + drp.SelectedValue + "'";
                DataSet dsproperty = BisProperty.GetPropertyData(getProperty);
                if (GeneratedCode != "")
                {
                    GeneratedCode += "-";
                }
                GeneratedCode += dsproperty.ReturnDataSetField("Code");

            }

            ViewModel.Search getDefine = new ViewModel.Search();
            getDefine.Filter = " and tblProductDefineDetailProduct.IDRet='" + hfIDProduct.Value + "' and tblDefineDetailProduct.GeneratedCode='" + GeneratedCode + "'";
            DataSet ds = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getDefine);
            return ds;
        }

        protected void btnDeleteOldPic_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblDefineDetailProduct OldDefineDetailProduct = new ViewModel.tblDefineDetailProduct();
                OldDefineDetailProduct.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                OldDefineDetailProduct.PicUrl = "";
                DeleteOldPic(OldDefineDetailProduct, hfDefineDetailPicUrl.Value);
                DivDeleteOldPic.Visible = false;
                FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }
        }

        public void DeleteOldPic(ViewModel.tblDefineDetailProduct objDefine, string OldUrl)
        {
            try
            {
                if (OldUrl == "")
                {
                    return;
                }
                else
                {
                    File.Delete(Server.MapPath(@"..\" + OldUrl));
                    int ret = BisDefineDetailProduct.UpdateDefineDetailProduct(objDefine);
                    if (ret <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در حذف عکس!');", true);
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OkMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }
        }

        public void setHeaderOfModal(string name)
        {
            lblHeaderOfModal.Text = name;
        }

        public void SetHfIDProduct(string IDProduct)
        {
            hfIDProduct.Value = IDProduct;
        }

        public bool ShowImage(string PicUrl)
        {
            if (PicUrl == "")
                return false;
            else
                return true;

        }

        public void OpenModalPropertyProductCategoryEvents()
        {
            btnSaveRuleChanges.Visible = false;
            btnAddRulePropertyAllRow.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myscr", "$('#" + ModalPropertyProductCategoryEvents.ClientID + "').modal('show');", true);

        }

        protected void btnDelOldIndexDescriptionPic_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblDefineDetailProduct OldDefineDetailProduct = new ViewModel.tblDefineDetailProduct();
                OldDefineDetailProduct.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                OldDefineDetailProduct.IndexDescriptionPicUrl = "";
                DeleteOldPic(OldDefineDetailProduct, hfIndexDescriptionPicUrl.Value);
                DivIndexDescriptionPic.Visible = false;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
            }
        }

        protected void FillSelectedTreeProductDefine()
        {
            ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
            getDefineDetailProduct.Filter = " and tblRelatedDefineDetailProduct.IDSourceDefineDetailProduct = '" + hfIDDefineDetailProduct.Value + "' OR tblRelatedDefineDetailProduct.IDDestinationDefineDetailProduct ='" + hfIDDefineDetailProduct.Value + "'";
            DataSet dsRelatedDefineDetailProduct = BisRelatedDefineDetailProduct.GetRelatedDefineDetailProductData(getDefineDetailProduct);
            ArrayList arrRelated = new ArrayList();
            foreach (DataRow dr in dsRelatedDefineDetailProduct.Tables[0].Rows)
            {
                if (dr["IDSourceDefineDetailProduct"].ToString() != hfIDDefineDetailProduct.Value && !arrRelated.Contains(dr["IDSourceDefineDetailProduct"].ToString()))
                {
                    arrRelated.Add(dr["IDSourceDefineDetailProduct"].ToString());
                }
                if (dr["IDDestinationDefineDetailProduct"].ToString() != hfIDDefineDetailProduct.Value && !arrRelated.Contains(dr["IDDestinationDefineDetailProduct"].ToString()))
                {
                    arrRelated.Add(dr["IDDestinationDefineDetailProduct"].ToString());
                }

            }
            TreeProduct.FillHFSelectedDefineDetail(arrRelated);
        }

        protected void FillSelectedTreeAccessoryProduct()
        {
            ViewModel.Search getAccessoryProduct = new ViewModel.Search();
            getAccessoryProduct.Filter = " AND tblAccessoryProduct.IDDefineDetailProduct = '" + hfIDDefineDetailProduct.Value + "' ";
            DataSet dsAccessoryProduct = BisAccessoryProduct.GetAccessoryProductData(getAccessoryProduct);
            ArrayList arrAccessoryProduct = new ArrayList();
            foreach (DataRow dr in dsAccessoryProduct.Tables[0].Rows)
            {
                arrAccessoryProduct.Add(dr["IDDefineDetailAccessory"].ToString());
            }
            TreeAccessory.FillHFSelectedDefineDetail(arrAccessoryProduct);
        }

        protected void grdRelatedDefineDetailProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRelatedDefineDetailProduct.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsRelatedDefineIndex"];
            grdRelatedDefineDetailProduct.DataSource = ds;
            grdRelatedDefineDetailProduct.DataBind();
        }

        protected void grdRelatedDefineDetailProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDRelatedDefineDetailProduct = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {

                        ViewModel.tblRelatedDefineDetailProduct DelRelatedDefineDetailProduct = new ViewModel.tblRelatedDefineDetailProduct();
                        DelRelatedDefineDetailProduct.IDRelatedDefineDetailProduct = IDRelatedDefineDetailProduct;
                        bool ret = BisRelatedDefineDetailProduct.DeleteRow(DelRelatedDefineDetailProduct);
                        if (ret)
                        {
                            FillSelectedTreeProductDefine();
                            FillGrdRelatedDefineDetailProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        protected void grdRelatedDefineDetailProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdRelatedDefineDetailProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void FillGrdRelatedDefineDetailProduct(Guid IDDefineDetailProduct)
        {
            try
            {
                ViewModel.tblRelatedDefineDetailProduct GetRelatedDefineDetailProduct = new ViewModel.tblRelatedDefineDetailProduct();
                GetRelatedDefineDetailProduct.IDDefineDetailProduct = IDDefineDetailProduct;
                DataSet dsRelatedDefine = BisRelatedDefineDetailProduct.GetAllRelations(GetRelatedDefineDetailProduct);
                grdRelatedDefineDetailProduct.DataSource = dsRelatedDefine;
                grdRelatedDefineDetailProduct.DataBind();
                Session["dsRelatedDefine"] = dsRelatedDefine;
                Session["dsRelatedDefineIndex"] = dsRelatedDefine;
            }
            catch
            {

            }
        }

        public bool CheckVisibleForProduct(Guid IDDefineDetailProduct)
        {
            if (hfCheckProduct.Value != "")
            {
                return hfCheckProduct.Value.StringToBool();
            }
            else
            {
                ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                getDefineDetailProduct.Filter = " AND tblProductDefineDetailProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct + "' AND IDRet IN (SELECT IDProduct FROM tblProduct WHERE Status <> 100)";
                DataSet dsProductDefineDetailProduct = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getDefineDetailProduct);
                if (!dsProductDefineDetailProduct.Null_Ds())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected void TreeAccessory_lbSelectedDefineClick(object sender, EventArgs e)
        {
            Guid IDDefineDetailProductAccessory = sender.ToString().StringToGuid();

            ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
            getDefineDetailProduct.Filter = " AND tblAccessoryProduct.IDDefineDetailProduct = '" + hfIDDefineDetailProduct.Value.StringToGuid() + "' AND tblAccessoryProduct.IDDefineDetailAccessory = '" + IDDefineDetailProductAccessory + "'";
            DataSet dsAccessoryProduct = BisAccessoryProduct.GetAccessoryProductData(getDefineDetailProduct);
            if (dsAccessoryProduct.Null_Ds())
            {
                ViewModel.tblAccessoryProduct NewAccessoryProduct = new ViewModel.tblAccessoryProduct();
                NewAccessoryProduct.IDAccessoryProduct = Guid.NewGuid();
                NewAccessoryProduct.IDDefineDetailAccessory = IDDefineDetailProductAccessory;
                NewAccessoryProduct.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                NewAccessoryProduct.Status = 1;
                bool ret = BisAccessoryProduct.AddAccessoryProduct(NewAccessoryProduct);
                if (ret)
                {
                    fillGrdAccessoryProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                    FillSelectedTreeAccessoryProduct();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            else
            {
                ViewModel.tblAccessoryProduct DelAccessoryProduct = new ViewModel.tblAccessoryProduct();
                DelAccessoryProduct.IDAccessoryProduct = dsAccessoryProduct.ReturnDataSetField("IDAccessoryProduct").StringToGuid();
                bool ret = BisAccessoryProduct.DeleteAccessoryProduct(DelAccessoryProduct);
                if (ret)
                {
                    fillGrdAccessoryProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                    FillSelectedTreeAccessoryProduct();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }

        }

        protected void grdAccessoryProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAccessoryProduct.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsAccessoryProductIndex"];
            grdAccessoryProduct.DataSource = ds;
            grdAccessoryProduct.DataBind();
        }

        protected void grdAccessoryProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdAccessoryProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdAccessoryProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDAccessoryProduct = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {
                        ViewModel.tblAccessoryProduct DelAccessoryProduct = new ViewModel.tblAccessoryProduct();
                        DelAccessoryProduct.IDAccessoryProduct = IDAccessoryProduct;
                        bool ret = BisAccessoryProduct.DeleteAccessoryProduct(DelAccessoryProduct);
                        if (ret)
                        {
                            fillGrdAccessoryProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                            FillSelectedTreeAccessoryProduct();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                    break;
            }
        }

        public void fillGrdAccessoryProduct(Guid IDDefineDetailProduct)
        {
            try
            {
                ViewModel.Search AccessorySearch = new ViewModel.Search();
                AccessorySearch.Filter = " and tblAccessoryProduct.IDDefineDetailProduct = '" + IDDefineDetailProduct + "' ";
                DataSet dsAccessory = BisAccessoryProduct.GetAccessoryProductData(AccessorySearch);
                grdAccessoryProduct.DataSource = dsAccessory;
                grdAccessoryProduct.DataBind();
                Session["dsAccessoryProductIndex"] = dsAccessory;
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void TreeProduct_lbSelectedDefineClick(object sender, EventArgs e)
        {
            Guid IDDestinationIDDefineDetailProduct = sender.ToString().StringToGuid();
            if (IDDestinationIDDefineDetailProduct != hfIDDefineDetailProduct.Value.StringToGuid())
            {
                ViewModel.Search getDefineDetailProduct = new ViewModel.Search();
                getDefineDetailProduct.Filter = " and (tblRelatedDefineDetailProduct.IDSourceDefineDetailProduct = '" + hfIDDefineDetailProduct.Value + "' AND tblRelatedDefineDetailProduct.IDDestinationDefineDetailProduct = '" + IDDestinationIDDefineDetailProduct + "') OR (tblRelatedDefineDetailProduct.IDDestinationDefineDetailProduct ='" + hfIDDefineDetailProduct.Value + "' AND tblRelatedDefineDetailProduct.IDSourceDefineDetailProduct = '" + IDDestinationIDDefineDetailProduct + "')";
                DataSet dsRelatedDefineDetailProduct = BisRelatedDefineDetailProduct.GetRelatedDefineDetailProductData(getDefineDetailProduct);
                if (dsRelatedDefineDetailProduct.Null_Ds())
                {
                    ViewModel.tblRelatedDefineDetailProduct NewRelatedDefineDetailProduct = new ViewModel.tblRelatedDefineDetailProduct();
                    NewRelatedDefineDetailProduct.IDRelatedDefineDetailProduct = Guid.NewGuid();
                    NewRelatedDefineDetailProduct.IDSourceDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                    NewRelatedDefineDetailProduct.IDDestinationDefineDetailProduct = IDDestinationIDDefineDetailProduct;
                    bool ret = BisRelatedDefineDetailProduct.AddRelatedDefineDetailProduct(NewRelatedDefineDetailProduct);
                    if (ret)
                    {
                        FillSelectedTreeProductDefine();
                        FillGrdRelatedDefineDetailProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
                else
                {
                    ViewModel.tblRelatedDefineDetailProduct DelRelatedDefineDetailProduct = new ViewModel.tblRelatedDefineDetailProduct();
                    DelRelatedDefineDetailProduct.IDRelatedDefineDetailProduct = dsRelatedDefineDetailProduct.ReturnDataSetField("IDRelatedDefineDetailProduct").StringToGuid();
                    bool ret = BisRelatedDefineDetailProduct.DeleteRow(DelRelatedDefineDetailProduct);
                    if (ret)
                    {
                        FillSelectedTreeProductDefine();
                        FillGrdRelatedDefineDetailProduct(hfIDDefineDetailProduct.Value.StringToGuid());
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorRelation", "bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> امکان ارتباط دادن یک محصول با خودش مقدور نیست!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        public void FillGrdDefineDetailProduct_All()
        {
            ViewModel.Search searchProductDefineDetailProduct = new ViewModel.Search();
            DataSet ds = BisProductDefineDetailProduct.GetProductDefineDetailProductData(searchProductDefineDetailProduct);
            grdProductDefineDetailProduct.DataSource = ds;
            grdProductDefineDetailProduct.DataBind();

            Session["dsDefineDetailProduct"] = ds;
            Session["dsDefineDetailProductIndex"] = ds;
        }

        public void InitialButtonsInAllDefineDetailProduct()
        {
            btnSearchRuleProperty.Visible = false;
            btnNewRuleProperty.Visible = false;
            btnAddRulePropertyAllRow.Visible = false;

        }

        protected void btnSearchDefineDetailProduct_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsDefineDetailProduct"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("PartNumber like '%" + txtSearchDefineDetailProduct.Text + "%'"))
            {
                dt.ImportRow(row);
            }
            grdDefineDetailProduct.DataSource = dt;  // baraye search
            grdDefineDetailProduct.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsDefineDetailProductIndex"] = ds;
        }

        protected void btnSaveRuleChanges_Click(object sender, EventArgs e)
        {
            try
            {

                bool Error = true;
                string strCombinationDescription = "";
                string strIDPropertyList = "";
                string strGenerateCode = "";
                foreach (RepeaterItem ri in rptParentProperty.Items)
                {
                    DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));
                    if (drp.SelectedValue == "0")
                    {
                        Error = true;
                        break;
                    }
                    else
                    {
                        if (strCombinationDescription != "")
                        {
                            strCombinationDescription += ", ";
                        }
                        strCombinationDescription += drp.SelectedItem.Text;

                        //list id property haie select shode
                        if (strIDPropertyList != "")
                        {
                            strIDPropertyList += ",";
                        }
                        strIDPropertyList += drp.SelectedValue;


                        ViewModel.Search getValueproperty = new ViewModel.Search();
                        getValueproperty.Filter = " and tblProperty.IDProperty = '" + drp.SelectedValue + "'";
                        DataSet dsProperty = BisProperty.GetPropertyData(getValueproperty);
                        if (strGenerateCode != "")
                        {
                            strGenerateCode += "-";
                        }
                        strGenerateCode += dsProperty.ReturnDataSetField("Code");

                        Error = false;
                    }
                }
                if (Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا از هر آیتم یکی را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
                else
                {

                    ViewModel.tblDefineDetailProduct EditDefine = new ViewModel.tblDefineDetailProduct();
                    EditDefine.IDDefineDetailProduct = hfIDDefineDetailProduct.Value.StringToGuid();
                    EditDefine.IDProduct = hfIDProduct.Value.StringToGuid();
                    EditDefine.GeneratedCode = strGenerateCode;
                    EditDefine.CombinationDescription = strCombinationDescription;
                    EditDefine.IDPropertyList = strIDPropertyList;
                    int retEditDefine = BisRuleProperty.UpdateRulesFromListID(EditDefine);
                    if (retEditDefine > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>ویرایش ترکیب با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                        btnSaveRuleChanges.Visible = false;
                        btnAddRulePropertyAllRow.Visible = true;
                        FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                    }


                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        protected void grdProductDefineDetailProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdProductDefineDetailProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdProductDefineDetailProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void fillGrdProductDefineDetailProduct()
        {
            ViewModel.Search searchProductDefineDetailProduct = new ViewModel.Search();
            searchProductDefineDetailProduct.Filter = " And  tblProductDefineDetailProduct.IDDefineDetailProduct = '" + hfIDDefineDetailProduct.Value + "'";
            DataSet ds = BisProductDefineDetailProduct.GetProductDefineDetailProductData(searchProductDefineDetailProduct);
            grdProductDefineDetailProduct.DataSource = ds;
            grdProductDefineDetailProduct.DataBind();
        }


        public void SetIDSupplier(string IDSupplier)
        {
            hfIDSupplier.Value = IDSupplier;

        }

        protected void btnSaveGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                bool Error = true;
                string strCombinationDescription = "";
                string strIDPropertyList = "";
                string strGenerateCode = "";
                int check = 0;
                foreach (RepeaterItem ri in rptParentProperty.Items)
                {
                    DropDownList drp = ((DropDownList)ri.FindControl("drpChildSubProperty"));
                    string hfMaterialType = ((HiddenField)ri.FindControl("hfMaterialType")).Value;

                    if (drp.SelectedValue == "0")
                    {
                        Error = true;
                        break;
                    }
                    else
                    {
                        if (strCombinationDescription != "")
                        {
                            strCombinationDescription += ", ";
                        }
                        strCombinationDescription += drp.SelectedItem.Text;

                        //list id property haie select shode
                        if (strIDPropertyList != "")
                        {
                            strIDPropertyList += ",";
                        }
                        strIDPropertyList += drp.SelectedValue;


                        ViewModel.Search getValueproperty = new ViewModel.Search();
                        getValueproperty.Filter = " and tblProperty.IDProperty = '" + drp.SelectedValue + "'";
                        DataSet dsProperty = BisProperty.GetPropertyData(getValueproperty);

                        if (hfMaterialType == check.ToString())
                        {
                            strGenerateCode += dsProperty.ReturnDataSetField("Value");
                        }
                        else
                        {
                            check = check + 1;
                            strGenerateCode += "-" + dsProperty.ReturnDataSetField("Value");
                        }

                        Error = false;
                    }
                }
                if (Error)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> لطفا از هر آیتم یکی را انتخاب کنید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
                else
                {

                    ViewModel.Search getDefine = new ViewModel.Search();
                    getDefine.Filter = " and tblProductDefineDetailProduct.IDRet='" + hfIDProduct.Value + "' and tblDefineDetailProduct.GeneratedCode='" + strGenerateCode + "'";
                    DataSet dsSearch = BisProductDefineDetailProduct.GetProductDefineDetailProductData(getDefine);
                    if (dsSearch.Null_Ds())
                    {

                        ViewModel.tblDefineDetailProduct AddDefine = new ViewModel.tblDefineDetailProduct();
                        AddDefine.IDDefineDetailProduct = Guid.NewGuid();
                        AddDefine.IDUser = IDUser;
                        AddDefine.IDUnit = "0283311f-b609-4556-b9cc-73ea8749e599".StringToGuid(); //PC
                        AddDefine.IDMadeCountry = "2c444b05-2b10-4bac-be85-6532db9cb082".StringToGuid(); ; //IR
                        AddDefine.PartNumber = "pigtail";
                        AddDefine.IDProduct = hfIDProduct.Value.StringToGuid();
                        AddDefine.CombinationDescription = strCombinationDescription;
                        AddDefine.GeneratedCode = strGenerateCode;
                        AddDefine.IDPropertyList = strIDPropertyList;
                        AddDefine.CreateDate = DateTime.Now;
                        AddDefine.Sort = 1;
                        int ret = BisDefineDetailProduct.AddDefineDetailProductSelectCases(AddDefine);
                        if (ret > 0)
                        {
                            FillGrdDefineDetailProduct(hfIDProduct.Value, hfCheckProduct.Value.StringToBool());
                            newRulePropertyFields();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ترکیب مورد نظر ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> این ترکیب قبلا ثبت شده است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }


                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        public void SetImageMasterProduct(string imgUrl)
        {
            imgMasterProduct.ImageUrl = imgUrl;
        }


    }
}