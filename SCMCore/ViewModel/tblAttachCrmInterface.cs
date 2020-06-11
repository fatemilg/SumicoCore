using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAttachCrmInterface : Model.IAttachCrmInterface
    {
        public Guid? IDAttachCrmInterface { get; set; }
        public Guid? IDAttachInterfaceCategory { get; set; }
        public Guid? IDAttachCrm { get; set; }
        public Guid? IDAttachSite { get; set; }
        public Guid? IDRet { get; set; }
        public Guid? IDUser { get; set; }
        public int? Order { get; set; }
        public int? Status { get; set; }

        public string AttachSiteCondition { get; set; }

    }
}