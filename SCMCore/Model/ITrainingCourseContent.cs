using System;
namespace SCMCore.Model
{
    public interface ITrainingCourseContent
    {
        Guid? IDTrainingCourseContent { get; set; }
        Guid? IDTrainingCourse { get; set; }
        Guid? IDContent { get; set; }
    }
}






