using System;

namespace SCMCore.Model
{
    public interface IUserRole
    {
        Guid? IDUserRole { get; set; }
        Guid? IDUser { get; set; }
        Guid? IDRole { get; set; }

        string UnicRoleName { get; set; }

    }
}