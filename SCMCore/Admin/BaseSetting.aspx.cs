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
using AjaxControlToolkit;

namespace SCMCore.Admin
{
    public partial class BaseSetting : System.Web.UI.Page
    {
        Bis.SettingMethod bisSetting = new Bis.SettingMethod();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGrdCurrency();
            }
        }


        protected void btnAddCurrency_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", " bootbox.alert({message: \"<p dir='rtl' style='color:#004179;font-size:17px;'> اشکال در برقراری ارتباط با دیتابیس!</p>\",title: \"<p style='text-align:right;direction:rtl'>خطا</p>\"});", true);

            }
        }

        public void fillGrdCurrency()
        {

        }
    }
}