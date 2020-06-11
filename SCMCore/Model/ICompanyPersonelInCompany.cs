using System;

namespace SCMCore.Model
{
    public interface ICompanyPersonelInCompany
    {
        Guid? IDCompanyPersonelInCompany { get; set; }
        Guid? IDCompany { get; set; }
        DateTime? CreateDate { get; set; }
        Guid? IDPersonelInCompany { get; set; }
        Guid? IDLevelOfPersonelInCompany { get; set; }
        Guid? IDOrganizationalPosition { get; set; }
        Guid? IDIntroducer { get; set; }
        int Status { get; set; }

    }
}