using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourseBatch : Model.ITrainingCourseBatch
    {
        public Guid? IDTrainingCourseBatch { get; set; }
        public string Name_Fa { get; set; }
        public string Description { get; set; }
        public int? IDX { get; set; }
    }
}






