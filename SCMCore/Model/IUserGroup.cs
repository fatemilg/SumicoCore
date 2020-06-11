using System;

namespace SCMCore.Model
{
    public interface IUserGroup
    {
        Guid IDUserGroup { get; set; }
        Guid? IDUser { get; set; }
        Guid? IDGroup { get; set; }

    }
}