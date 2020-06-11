using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCMCore.ExtensionMethod;
using System.IO;

namespace SCMCore.Admin.UserControl
{
    public partial class FileUpload : System.Web.UI.UserControl
    {
        public string ControlID;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
                ControlID = "a" + Guid.NewGuid().ToString().Replace("-", "");
                hfControlID.Value = ControlID;
                CustomFileUpload.Attributes.Add("onchange", ControlID + "CustomFileUploadOnChange()");
                clearFile.Attributes.Add("onclick", ControlID + "ClearFile()");
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Upload", hfControlID.Value + "UploadFiles('" + CustomFileUpload.ClientID + "','" + Guid.NewGuid() + "');", true);
        }

        public string MoveFile(string Path)
        {
            if (Directory.Exists(Server.MapPath(Path)) && hfFilelName.Value != "")
            {
                File.Move(Server.MapPath("TempUploadFile") + @"\" + hfFilelName.Value, Server.MapPath(Path) + @"\" + hfFilelName.Value);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Reset" + hfControlID.Value, hfControlID.Value + "ResetControl();", true);
                return (Path + @"\" + hfFilelName.Value).Replace(@"..\", "");
            }
            else
            {
                return "";
            }
        }

        public void ClearFileUpload()
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Upload", hfControlID.Value + "ClearFileUpload();", true);

        }

    }
}