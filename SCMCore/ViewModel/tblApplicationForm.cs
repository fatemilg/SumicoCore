using System;
using System.ComponentModel.DataAnnotations;

namespace SCMCore.ViewModel
{
    public class tblApplicationForm : Model.IApplicationForm
    {
        public Guid? IDApplicationForm { get; set; }
        public Guid? IDApplicationFormType { get; set; }
        public int? IDX { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string BirthCity { get; set; }

        public string BirthYear { get; set; }
        public string BirthCityFather { get; set; }
        public string FatherJob { get; set; }
        public string Grade { get; set; }
        public string FieldStudy { get; set; }
        public string University { get; set; }
        public string GraduationYear { get; set; }
        public string GPA { get; set; }
        public string DiplomFieldStudy { get; set; }
        public string Residence { get; set; }
        public string TotalExperience { get; set; }
        public string SoftwareExperience { get; set; }
        public string EmploymentStatus { get; set; }
        public string RequestedSalary { get; set; }
        public string Mobile { get; set; }
    }
}