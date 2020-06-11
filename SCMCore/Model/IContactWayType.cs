using System;

namespace SCMCore.Model
{
    public interface IContactWayType
    {
        Guid? IDContactWayType { get; set; }
        string Name_Fa { get; set; }
        int? Status { get; set; }
    }
}