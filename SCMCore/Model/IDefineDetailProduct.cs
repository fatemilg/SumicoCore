using System;

namespace SCMCore.Model
{
    public interface IDefineDetailProduct :IProduct
    {
        Guid? IDDefineDetailProduct { get; set; }        
        Guid? IDUser { get; set; }
        Guid? IDUnit { get; set; }
        Guid? IDMadeCountry { get; set; }
        int? IDX { get; set; }
        string CombinationDescription { get; set; }
        string TechnicalDescription_Fa { get; set; }
        string TechnicalDescription { get; set; }
        string ShortTechnicalDescription_Fa { get; set; }
        string ShortTechnicalDescription { get; set; }
        string PartNumber { get; set; }
        string GeneratedCode { get; set; }
        string MetaTag { get; set; }
        string MetaDescription { get; set; }
        string DescriptionInSite_En { get; set; }  
        string DescriptionInSite_Fa { get; set; }
        string PicUrl { get; set; }
        int? Sort { get; set; }
        bool? ShowInSite { get; set; }
        bool? SpecialDefine { get; set; }
        DateTime? CreateDate { get; set; }
        bool? UnderSpot { get; set; }
        string IDPropertyList { get; set; }
        string IndexDescriptionText { get; set; }
        string IndexDescriptionPicUrl { get; set; }
        int? IDXProduct { get; set; }
        string JsonDefineDetailProduct { get; set; }
        string CompareList { get; set; }
    }
}