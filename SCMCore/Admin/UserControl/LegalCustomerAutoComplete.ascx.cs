using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCMCore.Admin.UserControl
{
    public partial class LegalCustomerAutoComplete : System.Web.UI.UserControl
    {
        public string hfAutoLegal = "";
        public string IDUser
        {
            get
            {
                return hfAutoLegal;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AutoCompleteLegalCustomer", "AutoCompleteLegalCustomer();", true);
        }

        public void NewFildsUser()
        {
            txtLegalCustomer.Text = "";
            hfIDLegalCustomer.Value = "";
        }

        protected void txtLegalCustomer_TextChanged(object sender, EventArgs e)
        {
            hfAutoLegal = hfIDLegalCustomer.Value;
           
        }
     

    }
}