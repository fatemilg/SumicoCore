using System;

namespace SCMCore.Model
{
    public interface IQouationFile
    {
        Guid? IDQouationFile { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDSupplier { get; set; }
        Guid? IDCurrency { get; set; }
        string Title { get; set; }
        string FileUrlExcel { get; set; }
        Int64? FileSizeExcel { get; set; }
        string FileUrlPdf { get; set; }
        Int64? FileSizePdf { get; set; }
        string FileUrlEmail { get; set; }
        Int64? FileSizeEmail { get; set; }
        int? RatioRial { get; set; }
        decimal? RatioTransfer { get; set; }
        decimal? RatioMarkup { get; set; }
        decimal? RatioChfToEu { get; set; }
        DateTime? OrigDate { get; set; }
        bool? ExcelMode { get; set; }
        string ExcelJsonQouation { get; set; }
        string JsonQouationFile { get; set; }

    }
}