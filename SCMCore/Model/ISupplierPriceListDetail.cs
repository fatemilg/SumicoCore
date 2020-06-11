using System;

namespace SCMCore.Model
{
    public interface ISupplierPriceListDetail
    {
        Guid? IDSupplierPriceListDetail { get; set; }
        Guid? IDSupplierPriceListFile { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        string PartNumber { get; set; }
        decimal? Price { get; set; }
        string ShortDescription { get; set; }
        int? FixedPrice { get; set; }
        int? MarkUp { get; set; }
        int? SalesPrice { get; set; }


    }
}