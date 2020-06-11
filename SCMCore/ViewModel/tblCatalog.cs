using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCatalog : Model.ICatalog
    {
        public Guid IDCatalog { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public Guid? IDAttachPic { get; set; }
        public Guid? IDAttachPDF { get; set; }
        public Guid? IDRet { get; set; }
        public int? Sort { get; set; }
        public int? Status { get; set; }
    }
}