using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModelSite
{
    public class SignUp : Model.IUserSite
    {

        public Guid? IDUser { get; set; }
        public Guid? IDPersonelInCompany { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید")]
        public string FName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        public string LName { get; set; }
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "کد ملی را وارد کنید")]
        public string NationalCode { get; set; }
        [Required(ErrorMessage = "پست الکترونیک را وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "تلفن همراه را وارد کنید")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string Password { get; set; }

        public object QuestionCustomer { get; set; }

    }
}