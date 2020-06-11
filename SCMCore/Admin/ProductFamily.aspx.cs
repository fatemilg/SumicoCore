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

namespace SCMCore.Admin
{
    public partial class ProductFamily : System.Web.UI.Page
    {
        Bis.ProductFamilyMethod BisProductFamily = new Bis.ProductFamilyMethod();
        Guid IDUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            DataSet dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrdProductFamily();
                NewProductFamilyFields();
            }
        }
        public void FillGrdProductFamily()
        {
            ViewModel.Search ProductFamilySearch = new ViewModel.Search();
            DataSet dsProductFamily = BisProductFamily.GetProductFamilyData(ProductFamilySearch);
            grdProductFamily.DataSource = dsProductFamily;
            grdProductFamily.DataBind();
        }
        protected void NewProductFamilyFields()
        {
            txtName_Fa.Text = txtName_En.Text = txtDescription_Fa.Text = txtDescription_En.Text = "";
            hfModeProductFamily.Value = "New";
        }
        protected void btnNewProductFamily_Click(object sender, EventArgs e)
        {
            NewProductFamilyFields();
        }

        protected void btnAddProductFamily_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                ViewModel.tblProductFamily newProductFamily = new ViewModel.tblProductFamily();
                newProductFamily.Name_Fa = txtName_Fa.Text.FixFarsi();
                newProductFamily.Name_En = txtName_En.Text.FixFarsi();
                newProductFamily.Description_Fa = txtDescription_Fa.Text.FixFarsi();
                newProductFamily.Description_En = txtDescription_En.Text.FixFarsi();
                newProductFamily.Status = 1;

                switch (hfModeProductFamily.Value)
                {
                    case "New":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddProductFamily.ToString(), IDUser);
                            if (check)
                            {
                                ViewModel.Search ProductFamilySearch = new ViewModel.Search();
                                ProductFamilySearch.Filter = " AND tblProductFamily.Name_En = '" + txtName_En.Text.FixFarsi().Trim() + "'";
                                DataSet dsProductFamily = BisProductFamily.GetProductFamilyData(ProductFamilySearch);
                                if (dsProductFamily.Null_Ds())
                                {
                                    newProductFamily.IDProductFamily = Guid.NewGuid();
                                    bool ret = BisProductFamily.AddProductFamily(newProductFamily);
                                    if (ret)
                                    {
                                        hfIDProductFamily.Value = newProductFamily.IDProductFamily.ToString();
                                        FillGrdProductFamily();
                                        NewProductFamilyFields();
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اطلاعات ثبت شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ثبت اطلاعات</p>\"});", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> نام وارد شده برای خانواده محصول تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }

                        break;
                    case "Edit":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.EditProductFamily.ToString(), IDUser);
                            if (check)
                            {
                                ViewModel.Search ProductFamilySearch = new ViewModel.Search();
                                ProductFamilySearch.Filter = " AND tblProductFamily.Name_En = '" + txtName_En.Text.FixFarsi().Trim() + "' AND tblProductFamily.IDproductFamily <> '" + hfIDProductFamily.Value + "'";
                                DataSet dsProductFamily = BisProductFamily.GetProductFamilyData(ProductFamilySearch);
                                if (dsProductFamily.Null_Ds())
                                {
                                    newProductFamily.IDProductFamily = hfIDProductFamily.Value.StringToGuid();
                                    bool result = BisProductFamily.UpdateProductFamily(newProductFamily);
                                    if (result)
                                    {
                                        FillGrdProductFamily();
                                        NewProductFamilyFields();
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'>اطلاعات ویرایش شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>ویرایش اطلاعات</p>\"});", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ویرایش اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> نام وارد شده برای خانواده محصو تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                        }
                        break;
                }

            }
        }

        protected void grdProductFamily_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDProductFamily = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Edit":
                    try
                    {
                        hfIDProductFamily.Value = IDProductFamily.ToString();
                        ViewModel.Search ProductFamilySearch = new ViewModel.Search();
                        ProductFamilySearch.Filter = " and tblProductFamily.IDProductFamily ='" + IDProductFamily + "'";
                        DataSet ds = BisProductFamily.GetProductFamilyData(ProductFamilySearch);
                        if (!ds.Null_Ds())
                        {
                            txtName_Fa.Text = ds.ReturnDataSetField("Name_Fa");
                            txtName_En.Text = ds.ReturnDataSetField("Name_En");
                            txtDescription_Fa.Text = ds.ReturnDataSetField("Description_Fa");
                            txtDescription_En.Text = ds.ReturnDataSetField("Description_En");

                            hfModeProductFamily.Value = "Edit";
                        }
                    }
                    catch
                    {
                       
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;
                case "Delete":
                    try
                    {
                        ViewModel.tblProductFamily DeleteProductFamily = new ViewModel.tblProductFamily();
                        DeleteProductFamily.IDProductFamily = IDProductFamily;
                        bool retDel = BisProductFamily.DeleteProductFamily(DeleteProductFamily);
                        if (retDel)
                        {
                            FillGrdProductFamily();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> حذف اطلاعات با موفقیت انجام شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف</p>\"});", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در حذف اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ErrorMessage", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }
                    break;


            }
        }

        protected void grdProductFamily_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdProductFamily_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdProductFamily_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

    }
}