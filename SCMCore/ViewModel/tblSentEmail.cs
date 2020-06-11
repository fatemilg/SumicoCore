using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SCMCore.ViewModel
{
    public class tblSentEmail : Model.ISentEmail
    {
        public Guid? IDSentEmail { get; set; }
        public Guid? IDSender { get; set; }
        public Guid? IDRet { get; set; }
        public int? IDXRet { get; set; }
        public string SmtpAddress { get; set; }
        public int? PortNumber { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string EmailStatus { get; set; }

        public Guid? IDLogUser { get; set; }
    }
}