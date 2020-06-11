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
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.IO;

namespace SCMCore.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        Guid IDUser;
        protected void Page_Init(object sender, EventArgs e)
        {
            DataSet dsUser = new DataSet();
            dsUser = (DataSet)Session["User"];
            IDUser = dsUser.ReturnDataSetField("IDUser").StringToGuid();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

       
       





    }
}