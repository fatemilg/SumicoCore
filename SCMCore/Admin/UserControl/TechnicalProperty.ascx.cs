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
using System.Collections;
using System.IO;
using System.Web.Services;

namespace SCMCore.Admin.UserControl
{
    public partial class TechnicalProperty : System.Web.UI.UserControl
    {
        Bis.DetailTechnicalPropertyMethod BisDetailTechnicalProperty = new Bis.DetailTechnicalPropertyMethod();

        public Guid IDUser;
        DataSet dsUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LoadJsTechnicalProperty", "LoadJsTechnicalProperty();", true);
        }
        protected void btnNewDetailTechnicalProperty_Click(object sender, EventArgs e)
        {
            newFieldsDetailTechnicalProperty();

        }
        public void newFieldsDetailTechnicalProperty()
        {
            txtOrderTechnicalProperty.Text = "";
            txtTechnicalProperty.Text = "";
            txtTechnicalValue_Fa.Text = "";
            txtTechnicalValue_En.Text = "";
            hfTechnicalPropertyAutoComplete.Value = "";
            hfModeDetailTechnicalProperty.Value = "New";
        }

        protected void btnAddDetailTechnicalProperty_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hfTechnicalPropertyAutoComplete.Value == Guid.Empty.ToString() || hfTechnicalPropertyAutoComplete.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert(' ویژگی فنی را از لیست انتخاب کنید!');", true);
                    return;
                }
                if (txtTechnicalValue_En.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert(' مقدار ویژگی فنی(En) را وارد کنید!');", true);
                    return;
                }


                ViewModel.tblDetailTechnicalProperty newDetailTechnicalProperty = new ViewModel.tblDetailTechnicalProperty();
                newDetailTechnicalProperty.Value_Fa = txtTechnicalValue_Fa.Text.FixFarsi();
                newDetailTechnicalProperty.Value_En = txtTechnicalValue_En.Text.FixFarsi();
                newDetailTechnicalProperty.IDRet = hfIDRet.Value.StringToGuid();
                newDetailTechnicalProperty.IDUser = IDUser;
                newDetailTechnicalProperty.CreateDate = DateTime.Now;
                newDetailTechnicalProperty.IDTechnicalProperty = hfTechnicalPropertyAutoComplete.Value.StringToGuid();
                newDetailTechnicalProperty.Status = 1;
                newDetailTechnicalProperty.Order = txtOrderTechnicalProperty.Text.StringToInt();
                switch (hfModeDetailTechnicalProperty.Value)
                {
                    case "New":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.AddDetailTechnicalProperty.ToString(), IDUser);
                            if (!check)
                            {

                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }

                            newDetailTechnicalProperty.IDDetailTechnicalProperty = Guid.NewGuid();
                            if (checkTechnicalProperty(hfTechnicalPropertyAutoComplete.Value.StringToGuid(), hfIDDetailTechnicalProperty.Value.StringToGuid()))
                            {

                                bool ret = BisDetailTechnicalProperty.AddDetailTechnicalProperty(newDetailTechnicalProperty);
                                if (ret)
                                {

                                    hfIDDetailTechnicalProperty.Value = newDetailTechnicalProperty.IDDetailTechnicalProperty.ToString();
                                    fillGrdDetailTechnicalProperty(hfIDRet.Value.StringToGuid());
                                    newFieldsDetailTechnicalProperty();

                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OkMessage", "alert('اطلاعات ثبت شد!');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                                }
                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert(' برای این محصول با این عنوان ویژگی قبلا ثبت شده است !');", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                    case "Edit":
                        try
                        {
                            bool check = SqlHelper.CheckAccess(EventName.ListofEvents.EditDetailTechnicalProperty.ToString(), IDUser);
                            if (!check)
                            {

                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                                return;
                            }
                            newDetailTechnicalProperty.IDDetailTechnicalProperty = hfIDDetailTechnicalProperty.Value.StringToGuid();
                            if (checkTechnicalProperty(hfTechnicalPropertyAutoComplete.Value.StringToGuid(), hfIDDetailTechnicalProperty.Value.StringToGuid()))
                            {
                                bool result = BisDetailTechnicalProperty.UpdateDetailTechnicalProperty(newDetailTechnicalProperty);
                                if (result)
                                {
                                    fillGrdDetailTechnicalProperty(hfIDRet.Value.StringToGuid());
                                    newFieldsDetailTechnicalProperty();

                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OkMessage", "alert('اطلاعات ویرایش شد!');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در ویرایش اطلاعات!');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert(' برای این محصول با این عنوان ویژگی قبلا ثبت شده است !');", true);

                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);

                        }

                        break;
                }

            }
        }
        protected void grdDetailTechnicalProperty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDDetailTechnicalProperty = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {

                case "Edit":
                    try
                    {

                        hfIDDetailTechnicalProperty.Value = IDDetailTechnicalProperty.ToString();
                        ViewModel.Search DetailTechnicalPropertySearch = new ViewModel.Search();
                        DetailTechnicalPropertySearch.Filter = " and tblDetailTechnicalProperty.IDDetailTechnicalProperty ='" + IDDetailTechnicalProperty + "'";

                        DataSet ds = BisDetailTechnicalProperty.GetDetailTechnicalPropertyData(DetailTechnicalPropertySearch);
                        if (!ds.Null_Ds())
                        {
                            txtTechnicalValue_Fa.Text = ds.ReturnDataSetField("Value_Fa");
                            txtTechnicalValue_En.Text = ds.ReturnDataSetField("Value_En");
                            hfTechnicalPropertyAutoComplete.Value = ds.ReturnDataSetField("IDTechnicalProperty");
                            txtTechnicalProperty.Text = ds.ReturnDataSetField("TechnicalPropertyCodeName_En");
                         
                            hfIDDetailTechnicalProperty.Value = ds.ReturnDataSetField("IDDetailTechnicalProperty");
                            hfModeDetailTechnicalProperty.Value = "Edit";
                            txtOrderTechnicalProperty.Text = ds.ReturnDataSetField("Order");


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "Delete":

                    try
                    {
                        bool check = SqlHelper.CheckAccess(EventName.ListofEvents.DeleteDetailTechnicalProperty.ToString(), IDUser);
                        if (!check)
                        {

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('شما به این رویداد اجازه دسترسی ندارید!');", true);
                            return;
                        }
                        ViewModel.tblDetailTechnicalProperty DeleteDetailTechnicalProperty = new ViewModel.tblDetailTechnicalProperty();
                        DeleteDetailTechnicalProperty.IDDetailTechnicalProperty = IDDetailTechnicalProperty;

                        bool retDel = BisDetailTechnicalProperty.DeleteDetailTechnicalProperty(DeleteDetailTechnicalProperty);
                        if (retDel)
                        {
                            fillGrdDetailTechnicalProperty(hfIDRet.Value.StringToGuid());
                            newFieldsDetailTechnicalProperty();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
            }
        }
        protected void grdDetailTechnicalProperty_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdDetailTechnicalProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void grdDetailTechnicalProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDetailTechnicalProperty.PageIndex = e.NewPageIndex;

            DataSet ds = (DataSet)Session["dsDetailTechnicalPropertyIndex"];
            grdDetailTechnicalProperty.DataSource = ds;
            grdDetailTechnicalProperty.DataBind();
        }

        public bool checkTechnicalProperty(Guid IDTechnicalProperty, Guid IDDetailTechnicalProperty)
        {
            ViewModel.Search DetailTechnicalPropertySearch = new ViewModel.Search();
      
            switch (hfModeDetailTechnicalProperty.Value)
            {
                case "New":
                    DetailTechnicalPropertySearch.Filter = " and tblDetailTechnicalProperty.IDRet ='" + hfIDRet.Value + "' and tblDetailTechnicalProperty.IDTechnicalProperty ='" + hfTechnicalPropertyAutoComplete.Value + "' ";
                    break;
                case "Edit":
                    DetailTechnicalPropertySearch.Filter = " and tblDetailTechnicalProperty.IDDetailTechnicalProperty <> '" + IDDetailTechnicalProperty + "' and tblDetailTechnicalProperty.IDRet ='" + hfIDRet.Value + "' and tblDetailTechnicalProperty.IDTechnicalProperty ='" + hfTechnicalPropertyAutoComplete.Value + "'";
                    break;
            }
            DataSet ds = BisDetailTechnicalProperty.GetDetailTechnicalPropertyData(DetailTechnicalPropertySearch);
            if(ds.Null_Ds())
            {
                return true;
            }
            else
            {
                return false; //tekrari ast
            }
        }

        public void fillGrdDetailTechnicalProperty(Guid IDRet)
        {
       
                    ViewModel.Search DetailTechnicalPropertySearch = new ViewModel.Search();
                    DetailTechnicalPropertySearch.Filter = " and tblDetailTechnicalProperty.IDRet = '" + IDRet + "'";
                    DetailTechnicalPropertySearch.Order = " order by tblDetailTechnicalProperty.[Order] asc";
                    DataSet dsDetailTechnicalProperty = BisDetailTechnicalProperty.GetDetailTechnicalPropertyData(DetailTechnicalPropertySearch);
                    grdDetailTechnicalProperty.DataSource = dsDetailTechnicalProperty;
                    grdDetailTechnicalProperty.DataBind();
                    Session["dsDetailTechnicalProperty"] = dsDetailTechnicalProperty;
                    Session["dsDetailTechnicalPropertyIndex"] = dsDetailTechnicalProperty;
         
     
        }

        public void SetHfIDRet(string IDRet)
        {
            hfIDRet.Value = IDRet;
        }
        public void setHeaderOfModal(string name)
        {
            lblHeaderOfModal.Text = name;
        }
    }
}