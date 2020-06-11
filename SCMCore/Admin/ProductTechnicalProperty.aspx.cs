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
    public partial class ProductTechnicalProperty : System.Web.UI.Page
    {
        Bis.TechnicalPropertyMethod BisTechnicalProperty = new Bis.TechnicalPropertyMethod();
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();

        public DataRow[] dr;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                hfMode.Value = "New";
                divInfo.Visible = false;
                fillgrdTechnicalProperty();
            }
        }

        public void fillgrdTechnicalProperty()
        {
            ViewModel.Search PropertySearch = new ViewModel.Search();
            DataSet dsProperty = BisTechnicalProperty.GetTechnicalPropertyData(PropertySearch);
            grdTechnicalProperty.DataSource = dsProperty;
            grdTechnicalProperty.DataBind();
            Session["dsProperty"] = dsProperty;
            Session["dsPropertyIndex"] = dsProperty;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ISExistTechnicalProperty())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('خطا.ویژگی فنی وارد شده تکراری است!');", true);
                    return;
                }

                ViewModel.tblTechnicalProperty newProperty = new ViewModel.tblTechnicalProperty();
                newProperty.Name_Fa = txtName.Text.FixFarsi();
                newProperty.Name_En = txtName_En.Text.FixFarsi();
                newProperty.Status = 1;


                switch (hfMode.Value)
                {
                    case "New":
                        try
                        {
                            bool check = checkAccess(EventName.ListofEvents.AddDetailTechnicalProperty.ToString());
                            if (!check)
                            {

                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                return;
                            }

                            newProperty.IDTechnicalProperty = Guid.NewGuid();
                            if (checkProperty(txtName_En.Text.Trim()))
                            {
                                bool ret = BisTechnicalProperty.AddTechnicalProperty(newProperty);
                                if (ret)
                                {
                                    txtName.Text = txtName_En.Text = "";
                                    hfMode.Value = "New";
                                    hfIDTechnicalProperty.Value = newProperty.IDTechnicalProperty.ToString();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ثبت شد!');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ثبت اطلاعات!');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('ویژگی وارد شده تکراری می باشد!');", true);
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
                            bool check = checkAccess(EventName.ListofEvents.EditDetailTechnicalProperty.ToString());
                            if (!check)
                            {

                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                                return;
                            }
                            newProperty.IDTechnicalProperty = hfIDTechnicalProperty.Value.StringToGuid();
                            if (checkProperty(txtName_En.Text.Trim()))
                            {
                                bool result = BisTechnicalProperty.UpdateTechnicalProperty(newProperty);
                                if (result)
                                {
                                    txtName.Text = txtName_En.Text = "";

                                    fillgrdTechnicalProperty();
                                    hfMode.Value = "New";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "OkMessage", "alert('اطلاعات ویرایش شد!');", true);

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در ویرایش اطلاعات!');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('ویژگی وارد شده تکراری می باشد!');", true);
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

        protected void grdTechnicalProperty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid IDTechnicalProperty = e.CommandArgument.ToString().StringToGuid();
            switch (e.CommandName)
            {

                case "Edit":
                    try
                    {

                        hfIDTechnicalProperty.Value = IDTechnicalProperty.ToString();
                        ViewModel.Search PropertySearch = new ViewModel.Search();
                        PropertySearch.Filter = " and tblTechnicalProperty.IDTechnicalProperty ='" + IDTechnicalProperty + "'";

                        DataSet ds = BisTechnicalProperty.GetTechnicalPropertyData(PropertySearch);
                        if (!ds.Null_Ds())
                        {
                            txtName.Text = ds.ReturnDataSetField("Name_Fa");
                            txtName_En.Text = ds.ReturnDataSetField("Name_En");
                            hfMode.Value = "Edit";

                            divTable.Visible = false;
                            divInfo.Visible = true;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در واکشی اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;
                case "Delete":

                    try
                    {
                        bool check = checkAccess(EventName.ListofEvents.DeleteDetailTechnicalProperty.ToString());
                        if (!check)
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Succsess", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> شما به این رویداد اجازه دسترسی ندارید!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);
                            return;
                        }
                        ViewModel.tblTechnicalProperty DeleteProperty = new ViewModel.tblTechnicalProperty();
                        DeleteProperty.IDTechnicalProperty = IDTechnicalProperty;

                        bool retDel = BisTechnicalProperty.DeleteTechnicalProperty(DeleteProperty);
                        if (retDel)
                        {

                            fillgrdTechnicalProperty();
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('حذف اطلاعات با موفقیت انجام شد!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در حذف اطلاعات!');", true);
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMessage", "alert('اشکال در برقراری ارتباط با دیتابیس!');", true);
                    }
                    break;


            }
        }
        protected void grdTechnicalProperty_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdTechnicalProperty_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void grdTechnicalProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTechnicalProperty.PageIndex = e.NewPageIndex;
            DataSet ds = (DataSet)Session["dsPropertyIndex"];
            grdTechnicalProperty.DataSource = ds;
            grdTechnicalProperty.DataBind();

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            newFields();
            hfMode.Value = "New";

        }
        protected void btnNew2_Click(object sender, EventArgs e)
        {
            divTable.Visible = false;
            divInfo.Visible = true;
            hfMode.Value = "New";
            newFields();
        }
        public void newFields()
        {
            txtName.Text = "";
            txtName_En.Text = "";

        }
        protected void btnGrid_Click(object sender, EventArgs e)
        {
            divTable.Visible = true;
            divInfo.Visible = false;
            fillgrdTechnicalProperty();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet dsSearch = (DataSet)Session["dsProperty"];
            DataTable dt = new DataTable();
            dt = dsSearch.Tables[0].Clone();
            foreach (DataRow row in dsSearch.Tables[0].Select("Name_Fa like '%" + txtSearch.Text.FixFarsi() + "%' or Name_En like '%" + txtSearch.Text + "%' "))
            {
                dt.ImportRow(row);
            }
            grdTechnicalProperty.DataSource = dt;  // baraye search
            grdTechnicalProperty.DataBind();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            Session["dsPropertyIndex"] = ds;
        }
        public bool checkProperty(string propertyName_En)
        {
            ViewModel.Search PropertySearch = new ViewModel.Search();
            if (hfMode.Value == "New")
            {

                PropertySearch.Filter = " and Name_En ='" + propertyName_En + "'";
            }
            if (hfMode.Value == "Edit")
            {
                PropertySearch.Filter = "and IDTechnicalProperty <>'" + hfIDTechnicalProperty.Value + "' and Name_En = '" + propertyName_En + "' ";
            }
            DataSet dsCheck = BisTechnicalProperty.GetTechnicalPropertyData(PropertySearch);
            if (dsCheck.Null_Ds())
            {
                return true; //می توان ثبت کرد

            }
            else
            {
                return false; 
            }

        }

        protected bool checkAccess(string eventName)
        {
            ViewModel.tblEventUser getUserEvent = new ViewModel.tblEventUser();
            DataSet dsUser = (DataSet)Session["User"];
            getUserEvent.IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
            getUserEvent.EventName = eventName;
            DataSet ds = BisEventUser.CheckForAccessInEventUser(getUserEvent);
            if (ds.Null_Ds())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected bool ISExistTechnicalProperty()
        {
            ViewModel.Search getTechnicalProperty = new ViewModel.Search();
            if (hfMode.Value == "New")
            {

                getTechnicalProperty.Filter = " and tblTechnicalProperty.Name_Fa = N'" + txtName_En.Text.FixFarsi() + "' ";
            }
            if (hfMode.Value == "Edit")
            {
                getTechnicalProperty.Filter = "and  tblTechnicalProperty.IDTechnicalProperty!='" + hfIDTechnicalProperty.Value + "' and tblTechnicalProperty.Name_Fa = N'" + txtName_En.Text.FixFarsi() + "' ";
            }
            DataSet dsCheck = BisTechnicalProperty.GetTechnicalPropertyData(getTechnicalProperty);
            if (dsCheck.Null_Ds())
            {
                return false;

            }
            else
            {
                return true;
            }
        }
    }
}