using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCMCore.Admin
{
    public partial class AllDefineDetailProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetAll_Click(object sender, EventArgs e)
        {
            DefineDetailProduct.FillGrdDefineDetailProduct_All();
            DefineDetailProduct.InitialButtonsInAllDefineDetailProduct();
            DefineDetailProduct.OpenModalPropertyProductCategoryEvents();
        }
    }
}