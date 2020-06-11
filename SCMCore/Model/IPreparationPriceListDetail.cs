using System;

namespace SCMCore.Model
{
    public interface IPreparationPriceListDetail
    {
         Guid? IDPreparationPriceListDetail { get; set; }
         Guid? IDPreparationPriceList { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        Int64? EndUserPrice { get; set; }
         decimal? MarkUpForSPlist { get; set; }
         decimal? MarkUpForQoutation { get; set; }
         decimal? MarkUpForPeyvast { get; set; }
         string PartNumber { get; set; }
         string ShortDescription { get; set; }

         Int64? SPFixedPrice { get; set; }
        decimal? SPMarkUp { get; set; }
        Int64? SPSalesPrice { get; set; }

        Int64? QOFixedPrice { get; set; }
        decimal? QOMarkUp { get; set; }
        Int64? QOSalesPrice { get; set; }

        Int64? PEYFixedPrice { get; set; }
        decimal? PEYMarkUp1 { get; set; }
        Int64? PEYSalesPrice1 { get; set; }

    }
}