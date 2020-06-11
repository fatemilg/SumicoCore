using System;

namespace SCMCore.Model
{
    public interface IAttachInterfaceCategory
    {
        Guid? IDAttachInterfaceCategory { get; set; }
        Guid? IDParent { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        int? Status { get; set; }

        int? IDXDefineDetailProduct { get; set; }
    }
}