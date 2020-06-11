using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblSiteVisit : Model.ISiteVisite
    {
        public Guid? IDSiteVisit { get; set; }
        public string TableName { get; set; }
        public Int64? IDRet { get; set; }
        public DateTime? VisitDate { get; set; }
        public Int64? VisitCount { get; set; }
    }
}