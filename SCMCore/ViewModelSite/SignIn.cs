using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModelSite
{
    public class SignIn : Model.IUserSite
    {
        public Guid? IDUser { get; set; }
        public Guid? IDPersonelInCompany { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string CompanyName { get; set; }
        public string NationalCode { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        [Required(ErrorMessage = "کد ملی را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string Password { get; set; }

    }
}