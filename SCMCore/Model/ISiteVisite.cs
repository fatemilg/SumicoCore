using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface ISiteVisite
    {
        Guid? IDSiteVisit { get; set; }
        string TableName { get; set; }
        Int64? IDRet { get; set; }
        DateTime? VisitDate { get; set; }
        Int64? VisitCount { get; set; }
    }
}
