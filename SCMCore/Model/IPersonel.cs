using System;

namespace SCMCore.Model
{
    public interface IPersonel : IUser
    {
         Guid? IDOrganizationPosition { get; set; }
         string FName { get; set; }
         string LName { get; set; }
         string IdentifyNumber { get; set; }
         string NationalCode { get; set; }
         bool? PowerUser { get; set; }
    }
}