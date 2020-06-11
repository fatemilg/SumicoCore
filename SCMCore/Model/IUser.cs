using System;

namespace SCMCore.Model
{
    public  interface IUser
    {
        Guid? IDUser { get; set; }
        Guid? IDCity { get; set; }
        Guid? IDState { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Mobile { get; set; }
        string WebSite { get; set; }
        string PicUrl { get; set; }
        bool? PersonelType { get; set; }
        bool? CustomerType { get; set; }
        bool? SupplierType { get; set; }
        bool? UserSiteType { get; set; }
        bool? Active { get; set; }
        int? Status { get; set; }

    }
}