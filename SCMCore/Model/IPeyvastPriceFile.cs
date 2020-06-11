using System;

namespace SCMCore.Model
{
    public interface IPeyvastPriceFile
    {
         Guid? IDPeyvastPriceFile { get; set; }
         Guid? IDPersonel { get; set; }
         Guid? IDSupplier { get; set; }
         Guid? IDCurrency { get; set; }
         string Title { get; set; }
         string FileUrlExcel { get; set; }
         Int64? FileSizeExcel { get; set; }
         decimal? CurrencyValue { get; set; }
         DateTime? OrigDate { get; set; }

         int? RatioRial { get; set; }
         decimal? RatioMarkup { get; set; }

         string ExcelJsonPeyvastPrice { get; set; }
         string JsonPartNumberInFile { get; set; }
         string JsonPeyvastFile { get; set; }

    }
}