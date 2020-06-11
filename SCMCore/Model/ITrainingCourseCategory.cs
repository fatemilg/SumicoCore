using System;
namespace SCMCore.Model
{
    public interface ITrainingCourseCategory
    {
        Guid? IDTrainingCourseCategory { get; set; }
        Guid? IDParent { get; set; }
        int? IDX { get; set; }
        string Name_Fa { get; set; }
        string PicUrl { get; set; }
        string AttachPDFUrl { get; set; }
        string ShortDescription { get; set; }
        string Caption { get; set; }
        string Description { get; set; }
        int? Status { get; set; }
    }
}






