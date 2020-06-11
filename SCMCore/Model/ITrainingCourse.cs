using System;
namespace SCMCore.Model
{
    public interface ITrainingCourse
    {
        Guid? IDTrainingCourse { get; set; }
        int? IDX { get; set; }
        Guid? IDTrainingCourseCategory { get; set; }
        string Name_Fa { get; set; }
        DateTime? EndDate { get; set; }
        int? Duration { get; set; }
        string PicUrl { get; set; }
        string Description { get; set; }
        int? Status { get; set; }
    }
}






