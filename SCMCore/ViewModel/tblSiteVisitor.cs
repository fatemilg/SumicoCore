using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblSiteVisitor:Model.ISiteVisitor
    {
        public Guid? IDSiteVisitor { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IP { get; set; }
        public string DeviceName { get; set; }
        public string BrowserName { get; set; }
        public string OS { get; set; }
    }
}