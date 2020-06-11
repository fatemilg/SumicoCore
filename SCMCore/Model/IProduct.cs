using System;

namespace SCMCore.Model
{
    public interface IProduct
    {
        Guid? IDProduct { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDSupplier { get; set; }
        Guid? IDProductCategory { get; set; }
        Guid? IDProductFamily { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        string MetaTags { get; set; }
        string MetaDescriptions { get; set; }
        string ProductUrl { get; set; }
        bool? Accessory { get; set; }
        string IndexDescriptionText { get; set; }
        string IndexDescriptionPicUrl { get; set; }
        int? Sort { get; set; }
        int? Status { get; set; }

        ///------------- For Advance Search
        string strIDProductCategory { get; set; }
        string strIDProperty { get; set; }
        string strIDSupplier { get; set; }
        string VendorName { get; set; }
        int? MaxPrice { get; set; }
        int? MinPrice { get; set; }
    }
}