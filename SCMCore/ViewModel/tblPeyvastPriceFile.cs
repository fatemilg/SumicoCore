using System;

namespace SCMCore.ViewModel
{
    public class tblPeyvastPriceFile : Model.IPeyvastPriceFile
    {
        public Guid? IDPeyvastPriceFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDCurrency { get; set; }
        public string Title { get; set; }
        public string FileUrlExcel { get; set; }
        public Int64? FileSizeExcel { get; set; }
        public decimal? CurrencyValue { get; set; }
        public DateTime? OrigDate { get; set; }

        public int? RatioRial { get; set; }
        public decimal? RatioMarkup { get; set; }

        public string ExcelJsonPeyvastPrice { get; set; }
        public string JsonPartNumberInFile { get; set; }
        public string JsonPeyvastFile { get; set; }

    }
}