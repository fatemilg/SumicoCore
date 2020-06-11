using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCMCore.Admin.UserControl
{
    public partial class RealCustomerAutoComplete : System.Web.UI.UserControl
    {
        public string hfAutoReal = "";
        public string IDUser
        {
            get
            {
                return hfAutoReal;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AutoCompleteRealCustomer", "AutoCompleteRealCustomer();", true);
        }

        public void NewFildsUser()
        {
            txtRealCustomer.Text = "";
            hfIDRealCustomer.Value = "";
        }

        protected void txtRealCustomer_TextChanged(object sender, EventArgs e)
        {
            hfAutoReal = hfIDRealCustomer.Value;
        }
    }
}