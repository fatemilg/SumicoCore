using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblLegalUser : Model.ILegalUser
    {
        public Guid? IDParentCompany { get; set; }
        public Guid? IDWorkType { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string PostalCode { get; set; }
        public string RegistrationCode { get; set; }
        public string NationalCode { get; set; }
        public int? RegistrationNumber { get; set; }
        public bool? ActiveMenuInSite { get; set; }
        public int? Sort { get; set; }
        public string MenuPicUrl { get; set; }
        public string MetaTag { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDCity { get; set; }
        public Guid? IDState { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string WebSite { get; set; }
        public string PicUrl { get; set; }
        public bool? PersonelType { get; set; }
        public bool? CustomerType { get; set; }
        public bool? SupplierType { get; set; }
        public bool? UserSiteType { get; set; }
        public bool? Active { get; set; }
        public int? Status { get; set; }
    }
}