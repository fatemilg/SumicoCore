using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCMCore.Admin.UserControl
{
    public partial class TextEditor : System.Web.UI.UserControl
    {
        public Guid EditorClientID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetText()
        {
            string RetValue = "";
            RetValue = hfContentOfSummerNote.Value;
            return RetValue;
        }
        public void SetText(string Text)
        {
            hfContentOfSummerNote.Value = Text;
            string strScript = "$('#" + pnlSummerNoteEditor.ClientID + "txtSummerNoteEditor').summernote({code:'" + Text + "',callbacks: {onChange: function (contents, $editable) {$('#" + hfContentOfSummerNote.ClientID + "').val(contents)}}});";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), EditorClientID.ToString(), strScript, true);
        }

        public void Initial()
        {
            EditorClientID = Guid.NewGuid();
            string strScript = "$('#" + pnlSummerNoteEditor.ClientID + "txtSummerNoteEditor').summernote({callbacks: {onChange: function (contents, $editable) {$('#" + hfContentOfSummerNote.ClientID + "').val(contents)}}});";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), EditorClientID.ToString(), strScript, true);
        }
    }
}