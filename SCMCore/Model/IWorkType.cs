using System;

namespace SCMCore.Model
{
    public interface IWorkType
    {
        Guid? IDWorkType { get; set; }
        Guid? ParentID { get; set; }
        string Name_Fa { get; set; }
        int? Status { get; set; }
    }
}