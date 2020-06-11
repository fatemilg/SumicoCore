using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IApplicationForm
    {
        Guid? IDApplicationForm { get; set; }
        Guid? IDApplicationFormType { get; set; }
        int? IDX { get; set; }
        string FName { get; set; }
        string LName { get; set; }
        string BirthCity { get; set; }

        string BirthYear { get; set; }
        string BirthCityFather { get; set; }
        string FatherJob { get; set; }
        string Grade { get; set; }
        string FieldStudy { get; set; }
        string University { get; set; }
        string GraduationYear { get; set; }
        string GPA { get; set; }
        string DiplomFieldStudy { get; set; }
        string Residence { get; set; }
        string TotalExperience { get; set; }
        string SoftwareExperience { get; set; }
        string EmploymentStatus { get; set; }
        string RequestedSalary { get; set; }
        string Mobile { get; set; }
    }
}