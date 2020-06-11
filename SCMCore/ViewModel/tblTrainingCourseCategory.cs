
using System;
namespace SCMCore.ViewModel
{
    public class tblTrainingCourseCategory : Model.ITrainingCourseCategory
    {
        public Guid? IDTrainingCourseCategory { get; set; }
        public Guid? IDParent { get; set; }
        public int? IDX { get; set; }
        public string Name_Fa { get; set; }
        public string PicUrl { get; set; }
        public string AttachPDFUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}






