using System;
namespace SCMCore.Model
{
    public interface IMaterialList
    {
        Guid? IDMaterialList { get; set; }
        Guid? IDParent { get; set; }
        Guid? IDUser { get; set; }
        string Title { get; set; }
        DateTime? CreateDate { get; set; }
    }
}






