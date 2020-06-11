using System;

namespace SCMCore.Model
{
    public interface IOrganizationalPosition
    {
         Guid IDOrganizationPosition { get; set; }
         string Name_Fa { get; set; }
         Guid? ParentID { get; set; }
         int? Status { get; set; }

    }
}