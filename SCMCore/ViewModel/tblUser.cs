using System;

namespace SCMCore.ViewModel
{
    public  class tblUser : Model.IUser
    {
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