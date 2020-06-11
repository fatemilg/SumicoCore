using System;
namespace SCMCore.Model
{
    public interface IMaterialListItem
    {
        Guid? IDMaterialListItem { get; set; }
        Guid? IDMaterialList { get; set; }
        Int64? Count { get; set; }
        DateTime? CreateDate { get; set; }
        Int64? IDX { get; set; }

    }
}






