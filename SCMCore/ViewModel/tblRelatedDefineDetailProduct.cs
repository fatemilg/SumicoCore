using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblRelatedDefineDetailProduct : Model.IRelatedDefineDetailProduct
    {
        public Guid? IDRelatedDefineDetailProduct { get; set; }
        public Guid? IDSourceDefineDetailProduct { get; set; }
        public Guid? IDDestinationDefineDetailProduct { get; set; }
        public Guid? IDDefineDetailProduct { get; set; } //For Search
        public int? IDXDefineDetailProduct { get; set; } //For Search

    }
}