using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCMCore.ExtensionMethod;

namespace SCMCore.Admin.UserControl
{
    public partial class TreeDropDown : System.Web.UI.UserControl
    {
        public event EventHandler tvDropDown_SelectedNode;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void filltvDropDown(DataSet ds, string PrimaryKey)
        {
            try
            {
                tvDropDown.Nodes.Clear();
                TreeNode newNode = new TreeNode("گروه های تعریف شده", Guid.Empty.ToString());
                tvDropDown.Nodes.Add(newNode);
                BindTree(ds, newNode, PrimaryKey, tvDropDown);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        public void SelectNode(string ID)
        {
            hfIDSelected.Value = ID;
            ResetNodes(tvDropDown.Nodes[0]);
            NodesRecursive(tvDropDown.Nodes[0], ID);
        }
        public void ResetNodes(TreeNode ParentNode)
        {
            ParentNode.Text = ParentNode.Text.RemoveHTMLTags();
            foreach (TreeNode SubNode in ParentNode.ChildNodes)
            {
                ResetNodes(SubNode);
            }
        }
        private void NodesRecursive(TreeNode ParentNode, string ID)
        {
            if (ParentNode.Value == ID)
            {
                txtTitle.Text = ParentNode.Text;
                ParentNode.Text = "<i style='color:gray'>" + ParentNode.Text + "</b>";
                ExpandParentNode(ParentNode);

            }
            else
            {
                foreach (TreeNode SubNode in ParentNode.ChildNodes)
                {
                    NodesRecursive(SubNode, ID);
                }
            }
        }
        private void ExpandParentNode(TreeNode tn)
        {
            if (tn != null)
            {
                tn.Expand();
                ExpandParentNode(tn.Parent);
            }

        }
        public string SelectedNode()
        {
            return hfIDSelected.Value;
        }
        private void BindTree(DataSet ds, TreeNode parentNode, string Key, System.Web.UI.WebControls.TreeView tv)
        {
            DataRow[] ChildRows;
            if (parentNode == null)
            {
                string strExpr = "IDParent = '" + Guid.Empty.ToString() + "'";
                ChildRows = ds.Tables[0].Select(strExpr);
            }
            else
            {
                string strExpr = "IDParent = '" + parentNode.Value.ToString() + "'";
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

        protected void tvDropDown_SelectedNodeChanged(object sender, EventArgs e)
        {
            hfIDSelected.Value = tvDropDown.SelectedNode.Value;
            txtTitle.Text = tvDropDown.SelectedNode.Text;
            ResetNodes(tvDropDown.Nodes[0]);
            NodesRecursive(tvDropDown.Nodes[0], hfIDSelected.Value);
            if (tvDropDown_SelectedNode != null)
            {
                tvDropDown_SelectedNode(tvDropDown.SelectedNode, e);
            }
            tvDropDown.SelectedNode.Selected = false;
        }
    }
}