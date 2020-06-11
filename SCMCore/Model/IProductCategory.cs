using System;

namespace SCMCore.Model
{
    public interface IProductCategory
    {
        Guid? IDProductCategory { get; set; }
        Guid? ParentID { get; set; }
        Guid? IDSupplier { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        string PicUrl { get; set; }
        int? Order { get; set; }
        string MetaTags { get; set; }
        string MetaDescriptions { get; set; }
        bool? ShowInSite { get; set; }
        int? Status { get; set; }
        int? IDXProductCategory { get; set; }
        int? IDXParentCategory { get; set; }
        int? IDXSupplier { get; set; }

    }
}