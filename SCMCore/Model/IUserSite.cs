using System;

namespace SCMCore.Model
{
    public interface IUserSite 
    {
        Guid? IDUser { get; set; }
        Guid? IDPersonelInCompany { get; set; }
        string FName { get; set; }
        string LName { get; set; }
        string CompanyName { get; set; }
        string NationalCode { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }

    }
}