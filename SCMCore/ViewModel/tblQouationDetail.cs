using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblQouationDetail : Model.IQouationDetail
    {
        public Guid? IDQouationDetail { get; set; }
        public Guid? IDQouationFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public string PartNumber { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? FixedPrice { get; set; }
        public int? MarkUp { get; set; }
        public int? SalesPrice { get; set; }

        public string AllDetailJson { get; set; }
        public string ItemsSelected { get; set; }
        public decimal? RatioChfToEu { get; set; }



    }
}