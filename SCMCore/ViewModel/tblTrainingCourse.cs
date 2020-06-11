using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourse : Model.ITrainingCourse
    {
        public Guid? IDTrainingCourse { get; set; }
        public int? IDX { get; set; }
        public Guid? IDTrainingCourseCategory { get; set; }
        public string Name_Fa { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Duration { get; set; }
        public string PicUrl { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}






