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
    public partial class TreeCategoryProduct : System.Web.UI.UserControl
    {
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.ProductDefineDetailProductMethod BisProductDefineDetailProduct = new Bis.ProductDefineDetailProductMethod();
        Bis.DetailAssignPropertyMethod BisDetailAssignProperty = new Bis.DetailAssignPropertyMethod();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialDataSource();
            }
        }

        public void InitialDataSource()
        {
            ViewModel.Search SearchProductCategory = new ViewModel.Search();
            SearchProductCategory.Filter = " AND tblProductCategory.ParentID = '" + Guid.Empty + "'";
            if (hfIDSupplier.Value != "")
            {
                SearchProductCategory.Filter += " and IDSupplier='" + hfIDSupplier.Value + "'";
            }

            SearchProductCategory.Order = " ORDER BY tblProductCategory.[Order]";
            DataSet dsProductCategory = BisProductCategory.GetProductCategoryDataShowInTree(SearchProductCategory);
            rptProductCategory.DataSource = dsProductCategory;
            rptProductCategory.DataBind();
        }

        protected void lbExpand_Click(object sender, EventArgs e)
        {
            LinkButton lbExpand = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)lbExpand.NamingContainer;
            string hfIDProductCategory = ((HiddenField)ri.FindControl("hfIDProductCategory")).Value;
            HiddenField hfExpand = (HiddenField)ri.FindControl("hfExpand");
            bool Expand = hfExpand.Value.StringToBool();
            hfExpand.Value = (!Expand).ToString();
            Repeater rptProductCategory = (Repeater)ri.FindControl("rptProductCategory");
            Repeater rptMasterProduct = ((Repeater)ri.FindControl("rptMasterProduct"));
            if (Expand)
            {
                lbExpand.Text = "<i class='fa fa-plus' style='font-size:15px'></i>";
                if (rptProductCategory != null)
                {
                    rptProductCategory.DataSource = null;
                    rptProductCategory.DataBind();
                }
                rptMasterProduct.DataSource = null;
                rptMasterProduct.DataBind();
            }
            else
            {
                lbExpand.Text = "<i class='fa fa-minus' style='font-size:15px'></i>";

                if (rptProductCategory != null)
                {
                    ViewModel.Search SearchProductCategory = new ViewModel.Search();
                    SearchProductCategory.Filter = " AND tblProductCategory.ParentID = '" + hfIDProductCategory + "'";
                    SearchProductCategory.Order = " ORDER BY tblProductCategory.[Order]";
                    DataSet dsProductCategory = BisProductCategory.GetProductCategoryDataShowInTree(SearchProductCategory);
                    rptProductCategory.DataSource = dsProductCategory;
                    rptProductCategory.DataBind();
                }
                FillRptMasterProduct(rptMasterProduct, hfIDProductCategory);
            }

        }

        protected void FillRptMasterProduct(Repeater rptMasterProduct, string hfIDProductCategory)
        {
            try
            {
                ViewModel.Search searchProduct = new ViewModel.Search();
                searchProduct.Filter = " And tblProduct.IDProductCategory = '" + hfIDProductCategory + "'";
                searchProduct.Order = " Order By tblProductCategory.[Order]";
                DataSet dsProduct = BisProduct.GetProductData(searchProduct);
                rptMasterProduct.DataSource = dsProduct;
                rptMasterProduct.DataBind();
            }
            catch
            {

            }
        }

        protected bool CheckIDInSelectedList(string IDRet)
        {
            ViewModel.Search searchProductDefineDetailProduct = new ViewModel.Search();
            searchProductDefineDetailProduct.Filter = " And tblProductDefineDetailProduct.IDRet = '" + IDRet + "' and tblProductDefineDetailProduct.IDDefineDetailProduct = '" + hfSelectedDefineDetail.Value + "'";
            DataSet ds = BisProductDefineDetailProduct.GetProductDefineDetailProductData(searchProductDefineDetailProduct);

            if (!ds.Null_Ds())
            {
                return true;
            }
            else
            {
                return false; ;

            }

        }

        public void lbSelectedMasterProduct_Click(object sender, EventArgs e)
        {
            LinkButton LbSelectMasterProduct = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)LbSelectMasterProduct.NamingContainer;
            string hfSelectedMasterProduct = ((HiddenField)ri.FindControl("hfSelectedMasterProduct")).Value;

            ViewModel.tblDetailAssignProperty checkEqualAssign = new ViewModel.tblDetailAssignProperty();
            checkEqualAssign.IDMasterProductMain = hfSelectedMasterProduct.StringToGuid();
            checkEqualAssign.IDDefineSelected = hfSelectedDefineDetail.Value.StringToGuid();
            DataSet dsCheckAssign = BisDetailAssignProperty.CheckAssignItemsInTowCollection(checkEqualAssign);
            if(!dsCheckAssign.Null_Ds())
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>  ساختار محصول ها باید یکی باشند!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }



            if (CheckIDInSelectedList(hfSelectedMasterProduct))
            {

                ViewModel.Search searchProductDefineDetailProduct = new ViewModel.Search();
                searchProductDefineDetailProduct.Filter = " And  tblProductDefineDetailProduct.IDDefineDetailProduct = '" + hfSelectedDefineDetail.Value + "'";
                DataSet ds = BisProductDefineDetailProduct.GetProductDefineDetailProductData(searchProductDefineDetailProduct);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> برای این پارت نامبر حداقل یک محصول باید ثبت شده باشد!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
                else
                {
                    ViewModel.tblProductDefineDetailProduct del = new ViewModel.tblProductDefineDetailProduct();
                    del.IDDefineDetailProduct = hfSelectedDefineDetail.Value.StringToGuid();
                    del.IDRet = hfSelectedMasterProduct.StringToGuid();
                    bool ret = BisProductDefineDetailProduct.DeleteProductDefineDetailProduct(del);
                    if (ret)
                    {
                        LbSelectMasterProduct.Text = "<i class='fa fa fa-square-o'></i>";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> خطا!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                }
            }
            else
            {

                ViewModel.tblProductDefineDetailProduct Add = new ViewModel.tblProductDefineDetailProduct();
                Add.IDProductDefineDetailProduct = Guid.NewGuid();
                Add.IDDefineDetailProduct = hfSelectedDefineDetail.Value.StringToGuid();
                Add.IDRet = hfSelectedMasterProduct.StringToGuid();
                bool ret = BisProductDefineDetailProduct.AddProductDefineDetailProduct(Add);
                if (ret)
                {
                    LbSelectMasterProduct.Text = "<i class='fa fa fa-check-square-o'></i>";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> خطا!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                }

            }

        }


        public void SetIDDefineDetailProduct(string IDDefineDetailProduct)
        {
            hfSelectedDefineDetail.Value = IDDefineDetailProduct;

        }
        public void SetIDSupplier(string IDSupplier)
        {
            hfIDSupplier.Value = IDSupplier;

        }
    }
}