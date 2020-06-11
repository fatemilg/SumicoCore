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
    public partial class TreeContentCategory : System.Web.UI.UserControl
    {
        Bis.ContentCategoryMethod BisContentCategory = new Bis.ContentCategoryMethod();
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
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
            ViewModel.Search SearchContentCategory = new ViewModel.Search();
            SearchContentCategory.Filter = " AND tblContentCategory.IDParent = '" + Guid.Empty + "'";
            DataSet dsContentCategory = BisContentCategory.GetContentCategoryData(SearchContentCategory);
            rptContentCategory.DataSource = dsContentCategory;
            rptContentCategory.DataBind();
        }

        protected void lbExpand_Click(object sender, EventArgs e)
        {
            LinkButton lbExpand = sender as LinkButton;
            RepeaterItem ri = (RepeaterItem)lbExpand.NamingContainer;
            string hfIDContentCategory = ((HiddenField)ri.FindControl("hfIDContentCategory")).Value;
            HiddenField hfExpand = (HiddenField)ri.FindControl("hfExpand");
            bool Expand = hfExpand.Value.StringToBool();
            hfExpand.Value = (!Expand).ToString();
            Repeater rptContentCategory = (Repeater)ri.FindControl("rptContentCategory");
            Repeater rptDefineDetailProduct = (Repeater)ri.FindControl("rptDefineDetailProduct");
            if (Expand)
            {
                lbExpand.Text = "<i class='fa fa-plus' style='font-size:15px'></i>";
                if (rptContentCategory != null)
                {
                    rptContentCategory.DataSource = null;
                    rptContentCategory.DataBind();

                    rptDefineDetailProduct.DataSource = null;
                    rptDefineDetailProduct.DataBind();
                }

            }
            else
            {
                lbExpand.Text = "<i class='fa fa-minus' style='font-size:15px'></i>";

                if (rptContentCategory != null)
                {
                    ViewModel.Search SearchContentCategory = new ViewModel.Search();
                    SearchContentCategory.Filter = " AND tblContentCategory.IDParent = '" + hfIDContentCategory + "'";
                    DataSet dsContentCategory = BisContentCategory.GetContentCategoryData(SearchContentCategory);
                    rptContentCategory.DataSource = dsContentCategory;
                    rptContentCategory.DataBind();

                    ViewModel.Search SearchDefineDetail = new ViewModel.Search();
                    SearchDefineDetail.Filter = " AND tblDefineDetailProduct.IDProduct = '" + hfIDContentCategory + "'";
                    DataSet dsDefineDetail = BisDefineDetailProduct.GetDefineDetailProductData(SearchDefineDetail);
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