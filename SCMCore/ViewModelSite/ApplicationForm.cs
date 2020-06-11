using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModelSite
{
    public class ApplicationForm : Model.IApplicationForm
    {
        public Guid? IDApplicationForm { get; set; }
        public Guid? IDApplicationFormType { get; set; }
        public int? IDX { get; set; }
        [Required(ErrorMessage = "نام را وارد کنید")]
        public string FName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        public string LName { get; set; }
        [Required(ErrorMessage = "محل تولد را وارد کنید")]
        public string BirthCity { get; set; }

        [Required(ErrorMessage = "سال تولد را وارد کنید")]
        public string BirthYear { get; set; }
        [Required(ErrorMessage = "محل تولد پدر را وارد کنید")]
        public string BirthCityFather { get; set; }
        [Required(ErrorMessage = "شغل پدر را وارد کنید")]
        public string FatherJob { get; set; }
        [Required(ErrorMessage = "مقطع تحصیلی را وارد کنید")]
        public string Grade { get; set; }
        [Required(ErrorMessage = "رشته تحصیلی را وارد کنید")]
        public string FieldStudy { get; set; }
        [Required(ErrorMessage = "نام دانشگاه را وارد کنید")]
        public string University { get; set; }
        [Required(ErrorMessage = "سال فارغ التحصیلی را وارد کنید")]
        public string GraduationYear { get; set; }
        [Required(ErrorMessage = "معدل مقطع تحصیلی را وارد کنید")]
        public string GPA { get; set; }
        [Required(ErrorMessage = "رشته تحصیلی دیپلم را وارد کنید")]
        public string DiplomFieldStudy { get; set; }
        [Required(ErrorMessage = "محدوده سکونت را وارد کنید")]
        public string Residence { get; set; }
        [Required(ErrorMessage = "آشنایی با نرم افزارهای مرتبط را وارد کنید")] 
        public string TotalExperience { get; set; }
        [Required(ErrorMessage = "سابقه کاری مرتبط را وارد کنید")]
        public string SoftwareExperience { get; set; }
        [Required(ErrorMessage = "وضعیت اشتغال در حال حاضر را وارد کنید")]
        public string EmploymentStatus { get; set; }
        [Required(ErrorMessage = "میزان حقوق درخواستی را وارد کنید")]
        public string RequestedSalary { get; set; }
        [Required(ErrorMessage = "تلفن همراه را وارد کنید")]
        public string Mobile { get; set; }
    }
}