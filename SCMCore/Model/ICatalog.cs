using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface ICatalog
    {
        Guid IDCatalog { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        Guid? IDAttachPic { get; set; }
        Guid? IDAttachPDF { get; set; }
        Guid? IDRet { get; set; }
        int? Sort { get; set; }
        int? Status { get; set; }
    }
}