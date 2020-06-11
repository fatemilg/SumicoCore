using System;

namespace SCMCore.Model
{
    public interface IPeyvastPriceDetail
    {
         Guid? IDPeyvastPriceDetail { get; set; }
         Guid? IDPersonel { get; set; }
         Guid? IDPeyvastPriceFile { get; set; }
         Guid? IDDefineDetailProduct { get; set; }
         string PartNumber { get; set; }
         int? Price { get; set; }
         int? CurrencyValue { get; set; }
    }
}