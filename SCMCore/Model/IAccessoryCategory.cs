using System;

namespace SCMCore.Model
{
    public interface IAccessoryCategory
    {
        Guid IDAccessoryCategory { get; set; }
        Guid? IDSupplier { get; set; }
        Guid? IDParent { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        int? Sort { get; set; }
        int? Status { get; set; }
    }
}