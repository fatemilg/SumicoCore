using System;

namespace SCMCore.ViewModel
{
    public class tblUserSite : Model.IUserSite
    {
        public Guid? IDUser { get; set; }
        public Guid? IDPersonelInCompany { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string CompanyName { get; set; }
        public string NationalCode { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }


        public string UserName { get; set; }
        public string Password { get; set; }
    }
}