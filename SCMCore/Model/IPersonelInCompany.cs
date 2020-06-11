using System;

namespace SCMCore.Model
{
    public interface IPersonelInCompany
    {
        Guid? IDPersonelInCompany { get; set; }
        int? IDX { get; set; }
        Guid? IDCompany { get; set; }
        Guid? IDOrganizationalPosition { get; set; }
        Guid? IDIntroducer { get; set; }
        Guid? IDLevelOfPersonelInCompany { get; set; }
        Guid? IDInterfaceSale { get; set; }
        string FName_Fa { get; set; }
        string LName_Fa { get; set; }
        string FName_En { get; set; }
        string LName_En { get; set; }
        string NationalCode { get; set; }
        string PicUrl { get; set; }
        DateTime? BirthDate { get; set; }
        int? Status { get; set; }
        bool? Sex { get; set; }
    }
}