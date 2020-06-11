using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface ISiteVisitor
    {
        Guid? IDSiteVisitor { get; set; }
        DateTime? VisitDate { get; set; }
        string IP { get; set; }
        string DeviceName { get; set; }
        string BrowserName { get; set; }
        string OS { get; set; }
    }
}
