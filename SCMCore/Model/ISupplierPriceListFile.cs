using System;

namespace SCMCore.Model
{
    public interface ISupplierPriceListFile
    {
        Guid? IDSupplierPriceListFile { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDSupplier { get; set; }
        Guid? IDCurrency { get; set; }
        string Title { get; set; }
        string FileUrl { get; set; }
        Int64? FileSize { get; set; }

        int? RatioRial { get; set; }
        decimal? RatioTransfer { get; set; }
        decimal? RatioMarkup { get; set; }

        string ExcelJsonSupplierPriceList { get; set; }
        string JsonSupplierPriceFile { get; set; }
    }
}