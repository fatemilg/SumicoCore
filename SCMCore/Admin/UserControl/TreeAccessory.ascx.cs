using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using System.Collections;


namespace SCMCore.Admin.UserControl
{
    public partial class TreeAccessory : System.Web.UI.UserControl
    {
        Bis.AccessoryCategoryMethod BisAccessoryCategory = new Bis.AccessoryCategoryMethod();
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
            ViewModel.Search SearchAccessoryCategory = new ViewModel.Search();
            SearchAccessoryCategory.Filter = " AND tblAccessoryCategory.IDParent = '" + Guid.Empty + "'";
            DataSet dsAccessoryCategory = BisAccessoryCategory.GetAccessoryCategoryData(SearchAccessoryCategory);
            rptAccessoryCategory.DataSource = dsAccessoryCategory;
            rptAccessoryCategory.DataBind();
        }

        protected void lbExpand_Click(object sender, EventArgs e)
        {
            LinkButton lbExpand = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)lbExpand.NamingContainer;
            string hfIDAccessoryCategory = ((HiddenField)ri.FindControl("hfIDAccessoryCategory")).Value;
            HiddenField hfExpand = (HiddenField)ri.FindControl("hfExpand");
            bool Expand = hfExpand.Value.StringToBool();
            hfExpand.Value = (!Expand).ToString();
            Repeater rptAccessoryCategory = (Repeater)ri.FindControl("rptAccessoryCategory");
            Repeater rptDefineDetailProduct = (Repeater)ri.FindControl("rptDefineDetailProduct");
            if (Expand)
            {
                lbExpand.Text = "<i class='fa fa-plus' style='font-size:15px'></i>";
                if (rptAccessoryCategory != null)
                {
                    rptAccessoryCategory.DataSource = null;
                    rptAccessoryCategory.DataBind();

                    rptDefineDetailProduct.DataSource = null;
                    rptDefineDetailProduct.DataBind();
                }

            }
            else
            {
                lbExpand.Text = "<i class='fa fa-minus' style='font-size:15px'></i>";

                if (rptAccessoryCategory != null)
                {
                    ViewModel.Search SearchAccessoryCategory = new ViewModel.Search();
                    SearchAccessoryCategory.Filter = " AND tblAccessoryCategory.IDParent = '" + hfIDAccessoryCategory + "'";
                    DataSet dsAccessoryCategory = BisAccessoryCategory.GetAccessoryCategoryData(SearchAccessoryCategory);
                    rptAccessoryCategory.DataSource = dsAccessoryCategory;
                    rptAccessoryCategory.DataBind();

                    ViewModel.Search SearchDefineDetail = new ViewModel.Search();
                    SearchDefineDetail.Filter = " AND tblProductDefineDetailProduct.IDRet = '" + hfIDAccessoryCategory + "'";
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