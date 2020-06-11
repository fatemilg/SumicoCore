using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAttachInterfaceCategory : Model.IAttachInterfaceCategory
    {
        public Guid? IDAttachInterfaceCategory { get; set; }
        public Guid? IDParent { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public int? Status { get; set; }

        public int? IDXDefineDetailProduct { get; set; }
    }
}