using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCMCore.Admin.UserControl
{
    public partial class PersonelAutoComplete : System.Web.UI.UserControl
    {
        public string IDUser
        {
            get
            {
                return hfIDPersonelAutoCompelete.Value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AutoCompleteUserFullName", "AutoCompleteUserFullName();", true);
        }

        public void NewFildsUser()
        {
            txtUserFullName.Text = "";
            hfIDPersonelAutoCompelete.Value = "";
        }

    }
}