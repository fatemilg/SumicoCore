using System;

namespace SCMCore.Model
{
    public interface IContentCategory
    {
        Guid IDContentCategory { get; set; }
        Guid? IDContentCategoryType { get; set; }
        Guid? IDParent { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        int? Sort { get; set; }
        int? Status { get; set; }
    }
}