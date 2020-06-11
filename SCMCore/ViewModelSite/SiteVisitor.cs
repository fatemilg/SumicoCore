using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModelSite
{
    public class SiteVisitor:Model.ISiteVisite
    {
        public Guid? IDSiteVisit { get; set; }
        public string TableName { get; set; }
        public Int64? IDRet { get; set; }
        public DateTime? VisitDate { get; set; }
        public Int64? VisitCount { get; set; }
        public Int64? IDSiteVisitor { get; set; }
        public string IP { get; set; }
        public string BrowserName { get; set; }
        public string OS { get; set; }
    }
}