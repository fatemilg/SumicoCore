using System;

namespace SCMCore.Model
{
    public interface IRole
    {
        Guid IDRole { get; set; }
        string Title { get; set; }
        int? Status { get; set; }
    }
}