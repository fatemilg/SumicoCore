using System;

namespace SCMCore.Model
{
    public interface ILegalUser : IUser
    {

         Guid? IDParentCompany { get; set; }
         Guid? IDWorkType { get; set; } //zamine faaliat
         string Name_Fa { get; set; }
         string Name_En { get; set; }
         string Description_Fa { get; set; }
         string Description_En { get; set; }
         string PostalCode { get; set; }
         string RegistrationCode { get; set; }
         string NationalCode { get; set; }
         int? RegistrationNumber { get; set; }
         bool? ActiveMenuInSite { get; set; }
         int? Sort { get; set; }
         string MenuPicUrl { get; set; }
         string MetaTag { get; set; }

    }
}