using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourseContent : Model.ITrainingCourseContent
    {
        public Guid? IDTrainingCourseContent { get; set; }
        public Guid? IDTrainingCourse { get; set; }
        public Guid? IDContent { get; set; }
    }
}






