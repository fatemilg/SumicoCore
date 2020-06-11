using System;

namespace SCMCore.Model
{
    public interface IRealUser : IUser
    {
  
        Guid? IDLegalUser { get; set; }
        Guid? IDOrganizationPosition { get; set; }
        Guid? IDWorkType { get; set; } //zamine faaliat
        string FName { get; set; }
        string LName { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        string IdentifyNumber { get; set; }
        string NationalCode { get; set; }
        bool? Sex { get; set; }


    }
}