using SCMCore.ExtensionMethod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Admin.UserControl
{
    public partial class TreeProduct : System.Web.UI.UserControl
    {
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
        Bis.ProductDefineDetailProductMethod BisProductDefineDetailProduct = new Bis.ProductDefineDetailProductMethod();

        public event EventHandler lbSelectedDefineClick;
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

        protected void lbExpanMasterProduct_Click(object sender, EventArgs e)
        {
            LinkButton lbExpanMasterProduct = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)lbExpanMasterProduct.NamingContainer;
            string hfIDProduct = ((HiddenField)ri.FindControl("hfIDProduct")).Value;
            HiddenField hfExpandMasterProduct = (HiddenField)ri.FindControl("hfExpandMasterProduct");
            bool Expand = hfExpandMasterProduct.Value.StringToBool();
            hfExpandMasterProduct.Value = (!Expand).ToString();
            Repeater rptDefineDetailProduct = (Repeater)ri.FindControl("rptDefineDetailProduct");
            if (Expand)
            {
                lbExpanMasterProduct.Text = "<i class='fa fa-plus' style='font-size:15px'></i>";
                if (rptDefineDetailProduct != null)
                {
                    rptDefineDetailProduct.DataSource = null;
                    rptDefineDetailProduct.DataBind();
                }
            }
            else
            {
                lbExpanMasterProduct.Text = "<i class='fa fa-minus' style='font-size:15px'></i>";
                if (rptProductCategory != null)
                {
                    ViewModel.Search SearchDefineDetail = new ViewModel.Search();
                    SearchDefineDetail.Filter = " AND tblProductDefineDetailProduct.IDRet = '" + hfIDProduct + "'";
                    DataSet dsDefineDetail = BisProductDefineDetailProduct.GetProductDefineDetailProductData(SearchDefineDetail);
                    rptDefineDetailProduct.DataSource = dsDefineDetail;
                    rptDefineDetailProduct.DataBind();
                }
            }
        }

        protected bool CheckIDInSelectedList(string strIDDefineDetailProduct)
        {
            if (hfSelectedDefineDetail.Value.Contains(strIDDefineDetailProduct))
            {
                return true;
            }
            else
            {
                return false; ;

            }
        }

        public void lbSelectDefineDetail_Click(object sender, EventArgs e)
        {
            LinkButton lbSelectDefineDetail = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)lbSelectDefineDetail.NamingContainer;
            string hfIDDefineDetailProduct = ((HiddenField)ri.FindControl("hfIDDefineDetailProduct")).Value;
            if (CheckIDInSelectedList(hfIDDefineDetailProduct))
            {
                hfSelectedDefineDetail.Value = hfSelectedDefineDetail.Value.Replace(hfIDDefineDetailProduct + ",", "");
                lbSelectDefineDetail.Text = "<i class='fa fa fa-square-o'></i>";
            }
            else
            {
                hfSelectedDefineDetail.Value += hfIDDefineDetailProduct + ",";
                lbSelectDefineDetail.Text = "<i class='fa fa fa-check-square-o'></i>";
            }

            if (lbSelectedDefineClick != null)
            {
                lbSelectedDefineClick(hfIDDefineDetailProduct, e);
            }
        }

        public List<string> SelectedDefineDetailProduct()
        {
            return hfSelectedDefineDetail.Value.Remove(hfSelectedDefineDetail.Value.Length - 1, 1).Split(',').ToList();
        }

        public void FillHFSelectedDefineDetail(ArrayList arrSelected)
        {
            hfSelectedDefineDetail.Value = "";
            foreach (string str in arrSelected)
            {
                hfSelectedDefineDetail.Value += str + ",";
            }
        }
    }


}