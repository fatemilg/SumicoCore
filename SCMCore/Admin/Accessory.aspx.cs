using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.Admin
{
    public partial class Accessory : System.Web.UI.Page
    {
        Bis.AccessoryCategoryMethod BisAccessoryCategory = new Bis.AccessoryCategoryMethod();
        Bis.LegalUserMethod BisLegalUser = new Bis.LegalUserMethod();
        Guid IDSupplier;
        DataSet dsUser;
        Guid IDUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
            if (Session["IDSupplierForAccessoryPage"] != null)
            {
                IDSupplier = Session["IDSupplierForAccessoryPage"].ToString().StringToGuid();
            }
            else
            {
                IDSupplier = Guid.Empty;
                Response.Redirect("LegalSupplier.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                filltvAccessoryCategory();
                NewFieldsAccessoryAccessory();
            }
            TreeDropDownEdit.tvDropDown_SelectedNode += new EventHandler(TreeDropDownEdit_SelectedNodeChanged);
        }

        protected void GetSupplierName(Guid IDSupplier)
        {
            ViewModel.Search getSupplier = new ViewModel.Search();
            getSupplier.Filter = " and tblLegalUser.IDUser = '" + IDSupplier + "'";
            DataSet dsSupplier = BisLegalUser.GetSupplierData(getSupplier);
            lblSupplierNameList.Text = lblSupplierNameNew.Text = dsSupplier.ReturnDataSetField("Name_Fa");
        }

        protected void btnNewAccessoryCategory_Click(object sender, EventArgs e)
        {
            NewFieldsAccessoryAccessory();
        }

        public void NewFieldsAccessoryAccessory()
        {
            txtAccessoryName_Fa.Text = "";
            txtAccessoryName_En.Text = "";
            txtSort.Text = "";
            TreeDropDownEdit.SelectNode(Guid.Empty.ToString());
            hfModeAccessoryCategory.Value = "New";
            hfIDAccessoryCategory.Value = "";
        }

        protected void btnAddAccessoryCategory_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                ViewModel.tblAccessoryCategory newAccessoryCategory = new ViewModel.tblAccessoryCategory();
                newAccessoryCategory.Name_Fa = txtAccessoryName_Fa.Text.FixFarsi();
                newAccessoryCategory.Name_En = txtAccessoryName_En.Text;
                newAccessoryCategory.IDParent = TreeDropDownEdit.SelectedNode().StringToGuid();
                newAccessoryCategory.Sort = txtSort.Text.StringToInt();
                newAccessoryCategory.Status = 1;
                switch (hfModeAccessoryCategory.Value)
                {
                    case "New":
                        try
                        {
                            newAccessoryCategory.IDAccessoryCategory = Guid.NewGuid();
                            newAccessoryCategory.IDSupplier = IDSupplier;
                            bool ret = BisAccessoryCategory.AddAccessoryCategory(newAccessoryCategory);
                            if (ret)
                            {
                                hfIDAccessoryCategory.Value = newAccessoryCategory.IDAccessoryCategory.ToString();
                                filltvAccessoryCategory();
                                NewFieldsAccessoryAccessory();
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
                            newAccessoryCategory.IDAccessoryCategory = hfIDAccessoryCategory.Value.StringToGuid();
                            bool result = BisAccessoryCategory.UpdateAccessoryCategory(newAccessoryCategory);
                            if (result)
                            {
                                newAccessoryCategory.IDAccessoryCategory = hfIDAccessoryCategory.Value.StringToGuid();
                                filltvAccessoryCategory();
                                NewFieldsAccessoryAccessory();
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

        public void filltvAccessoryCategory()
        {
            try
            {
                GetSupplierName(IDSupplier);

                ViewModel.Search searchAccessoryCategory = new ViewModel.Search();
                searchAccessoryCategory.Filter = " And tblAccessoryCategory.IDSupplier='" + IDSupplier + "'";
                searchAccessoryCategory.Order = " Order By tblAccessoryCategory.[Sort]";
                DataSet ds = BisAccessoryCategory.GetAccessoryCategoryData(searchAccessoryCategory);
                TreeDropDownEdit.filltvDropDown(ds, "IDAccessoryCategory");

                tvAccessoryCategory.Nodes.Clear();
                TreeNode newNode = new TreeNode("گروه های تعریف شده", Guid.Empty.ToString());
                tvAccessoryCategory.Nodes.Add(newNode);
                BindTree(ds, newNode, "IDAccessoryCategory", tvAccessoryCategory);
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

        protected void TreeDropDownEdit_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void tvAccessoryCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvAccessoryCategory.SelectedNode.Value != Guid.Empty.ToString())
            {
                lblNodeName.Text = tvAccessoryCategory.SelectedNode.Text;
                hfIDAccessoryCategory.Value = tvAccessoryCategory.SelectedNode.Value.RemoveHTMLTags();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "myscr", "$('#ModalNavigationOnAccessory').modal('show');", true);
            }
            tvAccessoryCategory.SelectedNode.Selected = false;

        }

        protected void btnReturnSupplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("LegalSupplier.aspx");
        }

        protected void btnDelAccessoryCategory_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.tblAccessoryCategory DelAccessoryCategory = new ViewModel.tblAccessoryCategory();
                DelAccessoryCategory.IDAccessoryCategory = hfIDAccessoryCategory.Value.StringToGuid();
                bool retDel = BisAccessoryCategory.DeleteAccessoryCategory(DelAccessoryCategory);
                if (retDel)
                {
                    filltvAccessoryCategory();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> خطا در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void btnEditAccessoryCategory_Click(object sender, EventArgs e)
        {
            try
            {
                hfModeAccessoryCategory.Value = "Edit";
                ViewModel.Search searchAccessoryCategory = new ViewModel.Search();
                searchAccessoryCategory.Filter = " And tblAccessoryCategory.IDAccessoryCategory='" + hfIDAccessoryCategory.Value + "'";
                DataSet ds = BisAccessoryCategory.GetAccessoryCategoryData(searchAccessoryCategory);
                if (!ds.Null_Ds())
                {
                    txtAccessoryName_En.Text = ds.ReturnDataSetField("Name_En");
                    txtAccessoryName_Fa.Text = ds.ReturnDataSetField("Name_Fa");
                    txtSort.Text = ds.ReturnDataSetField("Sort");
                    TreeDropDownEdit.SelectNode(ds.ReturnDataSetField("IDParent"));
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "CloseModal", "$('#ModalNavigationOnAccessory').modal('hide');", true);
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
        }

        protected void btnPropertyAssignAccessoryCategory_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.Search searchAccessoryCategory = new ViewModel.Search();
                searchAccessoryCategory.Filter = " And tblAccessoryCategory.IDAccessoryCategory='" + hfIDAccessoryCategory.Value + "'";
                DataSet ds = BisAccessoryCategory.GetAccessoryCategoryData(searchAccessoryCategory);

                DetailAssignProperty.fillTreeDropDownProperties();
                DetailAssignProperty.fillGrdDetailAssignProperty(hfIDAccessoryCategory.Value);
                DetailAssignProperty.SetHfIDRet(hfIDAccessoryCategory.Value);
                string header = " محصول " + ds.ReturnDataSetField("Name_En");
                DetailAssignProperty.setHeaderOfModal(header);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "CloseModal", "$('#ModalNavigationOnAccessory').modal('hide');", true);
                DetailAssignProperty.OpenModal();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

        protected void btnAddAccessoryInTree_Click(object sender, EventArgs e)
        {
            try
            {
                ViewModel.Search searchAccessoryCategory = new ViewModel.Search();
                searchAccessoryCategory.Filter = " And tblAccessoryCategory.IDAccessoryCategory='" + hfIDAccessoryCategory.Value + "'";
                DataSet ds = BisAccessoryCategory.GetAccessoryCategoryData(searchAccessoryCategory);

                string Header = ds.ReturnDataSetField("Name_En");
                DefineDetailProduct.setHeaderOfModal(Header);
                DefineDetailProduct.SetHfIDProduct(hfIDAccessoryCategory.Value);
                DefineDetailProduct.FillrptParentProperty(hfIDAccessoryCategory.Value);
                DefineDetailProduct.FillGrdDefineDetailProduct(hfIDAccessoryCategory.Value,false);
                DefineDetailProduct.OpenModalPropertyProductCategoryEvents();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "CloseModal", "$('#ModalNavigationOnAccessory').modal('hide');", true);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }
        }

    }
}