using System;
namespace SCMCore.Model
{
    public interface ITrainingCourseUser
    {
        Guid? IDTrainingCourseUser { get; set; }
        Guid? IDTrainingCourse { get; set; }
        Guid? IDUser { get; set; }
        string CertificationID { get; set; }
        Guid? IDWorkType { get; set; }
    }
}






