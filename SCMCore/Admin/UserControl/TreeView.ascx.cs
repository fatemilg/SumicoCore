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

namespace SCMCore.Admin.UserControl
{
    public partial class TreeView : System.Web.UI.UserControl
    {
        private DataSet _Datasource = new DataSet();
        private string _ParentID;
        private string _DataValueField;
        private string _DataTextField;
        public string DataTextField
        {
            get { return _DataTextField; }
            set { _DataTextField = value; }
        }
        public string DataValueField
        {
            get { return _DataValueField; }
            set { _DataValueField = value; }
        }
        public string ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public DataSet DataSource
        {
            get { return _Datasource; }
            set { _Datasource = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TreeView1.Nodes.Clear();
                BindTree(_Datasource, null);
                TreeView1.DataBind();
            }

        }
        private void BindTree(DataSet ds, TreeNode parentNode)
        {
            if (!ds.Null_Ds())
            {
                DataRow[] ChildRows;
                if (parentNode == null)
                {
                    string strExpr = _ParentID + "='" + Guid.Empty.ToString() + "'";
                    ChildRows = ds.Tables[0].Select(strExpr);
                }
                else
                {
                    string strExpr = _ParentID + "='" + parentNode.Value.ToString() + "'";
                    ChildRows = ds.Tables[0].Select(strExpr);
                }
                foreach (DataRow dr in ChildRows)
                {
                    TreeNode newNode = new TreeNode(dr[_DataTextField].ToString(), dr[_DataValueField].ToString());
                    if (parentNode == null)
                    {
                        TreeView1.Nodes.Add(newNode);
                    }
                    else
                    {
                        parentNode.ChildNodes.Add(newNode);
                    }
                    BindTree(ds, newNode);
                }
            }
        }
        public string SelectedValue
        {
            get { return TreeView1.SelectedValue; }
        }


    }
}