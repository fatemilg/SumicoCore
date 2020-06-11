using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblSetting : Model.ISetting
    {
        public int IDSetting { get; set; }
        public string EmailSender { get; set; }
        public string SenderPassword { get; set; }
        public string EmailReciver { get; set; }
        public string SmtpAddress { get; set; }
        public string Port { get; set; }
    }
}