using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourseBatchTrainingCourse : Model.ITrainingCourseBatchTrainingCourse
    {
        public Guid? IDTrainingCourseBatchTrainingCourse { get; set; }
        public Guid? IDTrainingCourseBatch { get; set; }
        public Guid? IDTrainingCourse { get; set; }
        public int? Sort { get; set; }
    }
}






