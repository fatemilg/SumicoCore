using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourseUser : Model.ITrainingCourseUser
    {
        public Guid? IDTrainingCourseUser { get; set; }
        public Guid? IDTrainingCourse { get; set; }
        public Guid? IDUser { get; set; }
        public string CertificationID { get; set; }
        public Guid? IDWorkType { get; set; }
    }
}






