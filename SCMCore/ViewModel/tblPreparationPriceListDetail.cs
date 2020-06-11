using System;

namespace SCMCore.ViewModel
{
    public class tblPreparationPriceListDetail : Model.IPreparationPriceListDetail
    {
        public Guid? IDPreparationPriceListDetail { get; set; }
        public Guid? IDPreparationPriceList { get; set; }
        public Int64? EndUserPrice { get; set; }
        public decimal? MarkUpForSPlist { get; set; }
        public decimal? MarkUpForQoutation { get; set; }
        public decimal? MarkUpForPeyvast { get; set; }
        public string PartNumber { get; set; }
        public string ShortDescription { get; set; }

        public Guid? IDDefineDetailProduct { get; set; }
        public decimal? Percentage { get; set; }

        public Int64? SPFixedPrice { get; set; }
        public decimal? SPMarkUp { get; set; }
        public Int64? SPSalesPrice { get; set; }
     
        public Int64? QOFixedPrice { get; set; }
        public decimal? QOMarkUp { get; set; }
        public Int64? QOSalesPrice { get; set; }

        public Int64? PEYFixedPrice { get; set; }
        public decimal? PEYMarkUp1 { get; set; }
        public Int64? PEYSalesPrice1 { get; set; }
    }
}