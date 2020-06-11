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


namespace SCMCore.Admin
{
    public partial class Menu : System.Web.UI.Page
    {
        Bis.MenuMethod BisMenu = new Bis.MenuMethod();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                filltvMenu();

            }
        }

        protected void filltvMenu()
        {
            try
            {
                ViewModel.Search searchMenu = new ViewModel.Search();
                searchMenu.Order = " ORder By tblMenu.[Order]";
                DataSet ds = BisMenu.GetMenuData(searchMenu);
                tvMenu.Nodes.Clear();
                TreeNode newNode = new TreeNode("لیست منو ها", Guid.Empty.ToString());
                tvMenu.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDMenu", tvMenu);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

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
                TreeNode newNode = new TreeNode(dr["Name_Fa"].ToString(), dr[Key].ToString());
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



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {


                if (Page.IsValid)
                {
                    ViewModel.tblMenu newMenu = new ViewModel.tblMenu();
                    newMenu.IDMenu = Guid.NewGuid();
                    newMenu.Name_Fa = txtName_Fa.Text.FixFarsi();
                    newMenu.Name_En = txtName_En.Text.FixFarsi();
                    newMenu.Active = chkActive.Checked;
                    newMenu.Order = txtOrder.Text.StringToInt();
                    newMenu.ParentID = hfIdMenu.Value.StringToGuid();
                    newMenu.MenuUrl = txtMenuUrl.Text;
                    newMenu.Status = 1;


                    bool ret = BisMenu.AddMenu(newMenu);
                    if (ret)
                    {
                        filltvMenu();
                        tvMenu.ExpandAll();
                        newFields();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت !</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);


                    }



                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);


            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            newFields();

        }
        public void newFields()
        {

            txtName_Fa.Text = "";
            txtName_En.Text = "";
            txtOrder.Text = "";
            txtMenuUrl.Text = "";

        }


        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblModalMenuName.Text = tvMenu.SelectedNode.Text;
            hfIdMenu.Value = tvMenu.SelectedNode.Value;
            fillObejctsForEdit(hfIdMenu.Value);
            filltvEditMenu();
            tvEditMenu.ExpandAll();

            ViewModel.Search SearchMenu = new ViewModel.Search();
            SearchMenu.Filter = " And tblMenu.IDMenu = '" + tvMenu.SelectedNode.Value + "'";
            DataSet dsSelectedMenu = BisMenu.GetMenuData(SearchMenu);
            SelectTreeViewNode(tvEditMenu.Nodes, dsSelectedMenu.ReturnDataSetField("ParentID"));
            ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalMenuEvents').modal('show');$('#ModalMenuEvents').css('overflow','hidden')", true);
            tvMenu.SelectedNode.Selected = false;
        }

        protected void SelectTreeViewNode(TreeNodeCollection tn, string Value)
        {
            foreach (TreeNode node in tn)
            {
                if (node.Value == Value)
                {
                    node.Selected = true;
                }
                else
                {
                    foreach (TreeNode childNode in node.ChildNodes)
                    {
                        if (childNode.Value == Value)
                        {
                            childNode.Selected = true;
                        }
                        else
                        {
                            SelectTreeViewNode(childNode.ChildNodes, Value);
                        }
                    }
                }
            }
        }



        protected void fillObejctsForEdit(string IDMenu)
        {
            ViewModel.Search getMenu = new ViewModel.Search();
            getMenu.Filter = " and tblMenu.IDMenu = '" + IDMenu + "'";
            DataSet ds = BisMenu.GetMenuData(getMenu);
            txtEditName_Fa.Text = ds.ReturnDataSetField("Name_Fa");
            txtEditName_En.Text = ds.ReturnDataSetField("Name_En");
            txtEditOrder.Text = ds.ReturnDataSetField("Order");
            txtEditMenuUrl.Text = ds.ReturnDataSetField("MenuUrl");
            chkEditActive.Checked = ds.ReturnDataSetField("Active").StringToBool();
        }
        protected void btnEditMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ViewModel.tblMenu UpdateMenu = new ViewModel.tblMenu();
                    UpdateMenu.IDMenu = hfIdMenu.Value.StringToGuid();
                    UpdateMenu.Name_Fa = txtEditName_Fa.Text.FixFarsi();
                    UpdateMenu.Name_En = txtEditName_En.Text.FixFarsi();
                    UpdateMenu.Active = chkEditActive.Checked;
                    UpdateMenu.Order = txtEditOrder.Text.StringToInt();
                    UpdateMenu.MenuUrl = txtEditMenuUrl.Text;
                    UpdateMenu.ParentID = tvEditMenu.SelectedNode.Value.StringToGuid();
                    UpdateMenu.Status = 1;

                    bool ret = BisMenu.UpdateMenu(UpdateMenu);
                    if (ret)
                    {
                        filltvMenu();
                        tvMenu.ExpandAll();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت ویرایش شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش !</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);


                    }



                }
            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);


            }
        }

        protected void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            ViewModel.tblMenu CheckMenu = new ViewModel.tblMenu();
            CheckMenu.IDMenu = hfIdMenu.Value.StringToGuid();
            DataSet dsCheckChilds = BisMenu.GetCompleteChildMenu(CheckMenu);
            if (dsCheckChilds.Tables[0].Select("IDMenu <> '" + hfIdMenu.Value + "'").Count() == 0)
            {
                ViewModel.tblMenu DeleteMenu = new ViewModel.tblMenu();
                try
                {
                    DeleteMenu.IDMenu = hfIdMenu.Value.StringToGuid();
                    bool ret = BisMenu.DeleteMenu(DeleteMenu);
                    if (ret)
                    {
                        filltvMenu();
                        tvMenu.ExpandAll();
                        ScriptManager.RegisterStartupScript(this, GetType(), "modal", "$('#ModalMenuEvents').modal('hide');", true);

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", "$('#ModalMenuEvents').modal('hide'); bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
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
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ابتدا باید منوهای پایین دستی این منو  حذف شوند!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void filltvEditMenu()
        {
            try
            {
                ViewModel.Search searchMenu = new ViewModel.Search();
                searchMenu.Order = " ORder By tblMenu.[Order]";
                DataSet ds = BisMenu.GetMenuData(searchMenu);
                tvEditMenu.Nodes.Clear();
                TreeNode newNode = new TreeNode("لیست منو ها", Guid.Empty.ToString());
                tvEditMenu.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDMenu", tvEditMenu);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }










    }
}