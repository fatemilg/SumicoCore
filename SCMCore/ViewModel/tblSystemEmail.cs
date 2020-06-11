using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblSystemEmail:Model.ISystemEmail
    {
        public Guid? IDSystemEmail { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP_Address { get; set; }
        public int? PortNumber { get; set; }
    }
}