using System;

namespace SCMCore.Model
{
    public interface IProperty
    {
        Guid IDProperty { get; set; }
        Guid? IDParent { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        int? Sort { get; set; }
        string PicUrl { get; set; }
        int? Status { get; set; }
    }
}