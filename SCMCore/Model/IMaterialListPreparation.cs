using System;

namespace SCMCore.Model
{
    public interface IMaterialListPreparation
    {
        Guid? IDMaterialListPreparation { get; set; }
        Guid? IDParent { get; set; }
        Guid? IDMaterialList { get; set; }
        DateTime? ShowDate { get; set; }
    }

}