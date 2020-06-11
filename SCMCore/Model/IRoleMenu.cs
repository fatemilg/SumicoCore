using System;

namespace SCMCore.Model
{
    public interface IRoleMenu
    {
        Guid IDRoleMenu { get; set; }
        Guid? IDMenu { get; set; }
        Guid? IDRole { get; set; }
        bool? Access { get; set; }
    }
}