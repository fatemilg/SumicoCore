using System;

namespace SCMCore.ViewModel
{
    public class tblQouationFile : Model.IQouationFile
    {
        public Guid? IDQouationFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDCurrency { get; set; }
        public string Title { get; set; }
        public string FileUrlExcel { get; set; }
        public Int64? FileSizeExcel { get; set; }
        public string FileUrlPdf { get; set; }
        public Int64? FileSizePdf { get; set; }
        public string FileUrlEmail { get; set; }
        public Int64? FileSizeEmail { get; set; }
        public int? RatioRial { get; set; }
        public decimal? RatioTransfer { get; set; }
        public decimal? RatioMarkup { get; set; }
        public decimal? RatioChfToEu { get; set; }
        public DateTime? OrigDate { get; set; }
        public bool? ExcelMode { get; set; }
        public string ExcelJsonQouation { get; set; }
        public string JsonQouationFile { get; set; }

    }
}