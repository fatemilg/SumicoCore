using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblSupplierPriceListDetail : Model.ISupplierPriceListDetail
    {
        public Guid? IDSupplierPriceListDetail { get; set; }
        public Guid? IDSupplierPriceListFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public string PartNumber { get; set; }
        public decimal? Price { get; set; }
        public string ShortDescription { get; set; }
        public int? FixedPrice { get; set; }
        public int? MarkUp { get; set; }
        public int? SalesPrice { get; set; }


    }
}