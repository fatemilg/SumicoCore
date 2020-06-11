using System;

namespace SCMCore.Model
{
    public interface ILevelOfPersonelInCompany
    {
         Guid? IDLevelOfPersonelInCompany { get; set; }
         string Title { get; set; }
         int? Sort { get; set; }
         int? Status { get; set; }
    }
}