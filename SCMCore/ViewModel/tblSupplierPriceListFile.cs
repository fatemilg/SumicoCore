using System;

namespace SCMCore.ViewModel
{
    public class tblSupplierPriceListFile : Model.ISupplierPriceListFile
    {
        public Guid? IDSupplierPriceListFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDCurrency { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public Int64? FileSize { get; set; }

        public int? RatioRial { get; set; }
        public decimal? RatioTransfer { get; set; }
        public decimal? RatioMarkup { get; set; }

        public string ExcelJsonSupplierPriceList { get; set; }
        public string JsonSupplierPriceFile { get; set; }
    }
}