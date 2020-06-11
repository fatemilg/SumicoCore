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
using System.Web.Services;
using System.Web.Hosting;

namespace SCMCore.Admin.UserControl
{
    public partial class DetailAssignProperty : System.Web.UI.UserControl
    {
        Bis.DetailAssignPropertyMethod BisDetailAssignProperty = new Bis.DetailAssignPropertyMethod();
        Bis.PropertyMethod BisProperty = new Bis.PropertyMethod();
        Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
        Bis.RulePropertyProductMethod BisRulepropertyProduct = new Bis.RulePropertyProductMethod();
        public Guid IDUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            IDUser = ((DataSet)Session["User"]).ReturnDataSetField("IDUser").StringToGuid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void OpenModal()
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "OpenModal", "$('#ModalPropertyAssign').modal('show');$('#ModalPropertyAssign').css('overflow','hidden')", true);
        }

        protected void txtSort_TextChanged(object sender, EventArgs e)
        {
            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.ChangeSortDetailAssignProperty.ToString(), IDUser);
            if (!check)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                return;
            }
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            int index = row.RowIndex;
            TextBox txtSort = (TextBox)grdDetailAssignProperty.Rows[index].FindControl("txtSort");
            HiddenField hfIDDetailAssignPropertyInGrid = (HiddenField)grdDetailAssignProperty.Rows[index].FindControl("hfIDDetailAssignPropertyInGrid");
            ViewModel.tblDetailAssignProperty updateBySort = new ViewModel.tblDetailAssignProperty();
            updateBySort.IDDetailAssignProperty = hfIDDetailAssignPropertyInGrid.Value.StringToGuid();
            updateBySort.Sort = txtSort.Text.StringToInt();
            bool ret = BisDetailAssignProperty.UpdateDetailAssignProperty(updateBySort);
            if (ret)
            {
                fillGrdDetailAssignProperty(hfIDRet.Value);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }
        protected void grdDetailAssignProperty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDDetailAssignProperty = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {
                        bool check = SqlHelper.CheckAccess(EventName.ListofEvents.DeleteDetailAssignProperty.ToString(), IDUser);
                        if (!check)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }
                        ViewModel.tblDetailAssignProperty deleteDetailAssignProperty = new ViewModel.tblDetailAssignProperty();
                        deleteDetailAssignProperty.IDDetailAssignProperty = IDDetailAssignProperty;
                        bool ret = BisDetailAssignProperty.DeleteDetailAssignProperty(deleteDetailAssignProperty);
                        if (ret)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اطلاعات با موفقیت حذف شد!</p>\",title: \"<p style='text-align:right;direction:rtl'>حذف اطلاعات</p>\"});", true);
                            fillGrdDetailAssignProperty(hfIDRet.Value);
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

                case "SetForShowInPorductCategory":
                    try
                    {
                        ViewModel.tblDetailAssignProperty SetForShowInPorductCategory = new ViewModel.tblDetailAssignProperty();
                        SetForShowInPorductCategory.IDDetailAssignProperty = IDDetailAssignProperty;
                        SetForShowInPorductCategory.IDRet = hfIDRet.Value.StringToGuid();
                        bool retUpdate = BisDetailAssignProperty.SetForShowInPorductCategory(SetForShowInPorductCategory);
                        if (retUpdate)
                        {
                            fillGrdDetailAssignProperty(hfIDRet.Value);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت Under Spot');", true);
                        }
                    }
                    catch { }
                    break;
            }
        }
        protected void grdDetailAssignProperty_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdDetailAssignProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void grdDetailAssignProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        public void fillGrdDetailAssignProperty(string IDRet)
        {
            try
            {
                SetHfIDRet(IDRet);
                ViewModel.Search getDetailAssignProperty = new ViewModel.Search();
                getDetailAssignProperty.Order = "Order by tblDetailAssignProperty.Sort ";
                getDetailAssignProperty.Filter = " and tblDetailAssignProperty.IDRet = '" + IDRet + "'";
                DataSet dsDetailAssignProperty = BisDetailAssignProperty.GetDetailAssignPropertyData(getDetailAssignProperty);
                grdDetailAssignProperty.DataSource = dsDetailAssignProperty;
                grdDetailAssignProperty.DataBind();
                Session["dsDetailAssignProperty"] = dsDetailAssignProperty;
                Session["dsDetailAssignPropertyIndex"] = dsDetailAssignProperty;
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
            }

        }
        public void fillTreeDropDownProperties()
        {
            ViewModel.Search getProperty = new ViewModel.Search();
            getProperty.Filter = " and tblProperty.IDProperty IN (SELECT IDParent FROM tblProperty)";
            DataSet dsProperty = BisProperty.GetPropertyData(getProperty);
            //drpPropertyInAssign.Items.Clear();
            //drpPropertyInAssign.DataSource = dsProperty;
            //drpPropertyInAssign.DataTextField = "Name_En";
            //drpPropertyInAssign.DataValueField = "IDProperty";
            //drpPropertyInAssign.DataBind();
            //drpPropertyInAssign.Items.Insert(0, new ListItem("-انتخاب کنید -", "00000000-0000-0000-0000-000000000000"));
            TreeDropDownProperties.filltvDropDown(dsProperty, "IDProperty");


        }

        public void setHeaderOfModal(string name)
        {
            lblHeaderOfModal.Text = name;
        }
        public void SetHfIDRet(string IDRet)
        {
            hfIDRet.Value = IDRet;
        }

        protected void grdDetailAssignProperty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hfIDProperty = ((HiddenField)e.Row.FindControl("hfIDProperty")).Value;
                ViewModel.Search getRuleProduct = new ViewModel.Search();
                getRuleProduct.Filter = " AND tblRulePropertyProduct.IDProduct = '" + hfIDRet.Value + "' AND tblRulePropertyProduct.IDProperty IN (SELECT IDProperty FROM dbo.tblProperty WHERE IDParent ='" + hfIDProperty + "')";
                DataSet ds = BisRulepropertyProduct.GetRulePropertyProductData(getRuleProduct);
                if (!ds.Null_Ds())
                {
                    ((Button)e.Row.FindControl("btnDel")).Visible = false;
                }
                else
                {
                    ((Button)e.Row.FindControl("btnDel")).Visible = true;
                }
            }
        }

        protected void TreeDropDownProperties_tvDropDown_SelectedNode(object sender, EventArgs e)
        {
            try
            {
                bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddDetailAssignProperty.ToString(), IDUser);
                if (!check)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                TreeNode SelectedNode = sender as TreeNode;
                if ( SelectedNode.ChildNodes.Count != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما تنها مجاز به انتخاب شاخه های نهایی این درخت هستید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                    return;
                }
                ViewModel.Search getAssignProperty = new ViewModel.Search();
                getAssignProperty.Filter = " and tblDetailAssignProperty.IDRet = '" + hfIDRet.Value + "' and tblDetailAssignProperty.IDProperty='" + TreeDropDownProperties.SelectedNode() + "'";
                DataSet dsAssign = BisDetailAssignProperty.GetDetailAssignPropertyData(getAssignProperty);
                if (dsAssign.Null_Ds())
                {
                    ViewModel.tblDetailAssignProperty NewDetailAssignProperty = new ViewModel.tblDetailAssignProperty();
                    NewDetailAssignProperty.IDDetailAssignProperty = Guid.NewGuid();
                    NewDetailAssignProperty.IDProperty = TreeDropDownProperties.SelectedNode().StringToGuid();
                    NewDetailAssignProperty.IDRet = hfIDRet.Value.StringToGuid();
                    NewDetailAssignProperty.Sort = 0;
                    bool resault = BisDetailAssignProperty.AddDetailAssignProperty(NewDetailAssignProperty);
                    if (resault)
                    {
                        fillGrdDetailAssignProperty(hfIDRet.Value);


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در ثبت اطلاعات!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> ویژگی انتخاب شده برای این گروه تکراری است!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }


    }
}