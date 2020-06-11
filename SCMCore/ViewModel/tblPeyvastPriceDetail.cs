using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPeyvastPriceDetail : Model.IPeyvastPriceDetail
    {
        public Guid? IDPeyvastPriceDetail { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDPeyvastPriceFile { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public string PartNumber { get; set; }
        public int? Price { get; set; }
        public int? CurrencyValue { get; set; }
    }
}