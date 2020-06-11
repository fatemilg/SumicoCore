using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblDefineDetailProduct : Model.IDefineDetailProduct
    {
        public Guid? IDDefineDetailProduct { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDUnit { get; set; }
        public Guid? IDMadeCountry { get; set; }
        public int? IDX { get; set; }
        public string CombinationDescription { get; set; }
        public string TechnicalDescription_Fa { get; set; }
        public string TechnicalDescription { get; set; }
        public string ShortTechnicalDescription_Fa { get; set; }
        public string ShortTechnicalDescription { get; set; }
        public string PartNumber { get; set; }
        public string GeneratedCode { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public string DescriptionInSite_En { get; set; }
        public string DescriptionInSite_Fa { get; set; }
        public string PicUrl { get; set; }
        public int? Sort { get; set; }
        public bool? ShowInSite { get; set; }
        public bool? SpecialDefine { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? UnderSpot { get; set; }
        public string IDPropertyList { get; set; }
        public string IndexDescriptionText { get; set; }
        public string IndexDescriptionPicUrl { get; set; }
        public int? IDXProduct { get; set; }
        public string JsonDefineDetailProduct { get; set; }
        public string CompareList { get; set; }
        public Guid? IDProduct { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDProductCategory { get; set; }
        public Guid? IDProductFamily { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string MetaTags { get; set; }
        public string MetaDescriptions { get; set; }
        public string ProductUrl { get; set; }
        public bool? Accessory { get; set; }
        public int? Status { get; set; }
        public string strIDProductCategory { get; set; }
        public string strIDProperty { get; set; }
        public string strIDSupplier { get; set; }
        public string VendorName { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
    }
}