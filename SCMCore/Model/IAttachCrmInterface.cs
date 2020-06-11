using System;

namespace SCMCore.Model
{
    public interface IAttachCrmInterface
    {
        Guid? IDAttachCrmInterface { get; set; }
        Guid? IDAttachInterfaceCategory { get; set; }
        Guid? IDAttachCrm { get; set; }
        Guid? IDAttachSite { get; set; }
        Guid? IDRet { get; set; }
        Guid? IDUser { get; set; }
        int? Order { get; set; }
        int? Status { get; set; }

        string AttachSiteCondition { get; set; }

    }
}