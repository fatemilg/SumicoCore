using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAccessoryProduct : Model.IAccessoryProduct
    {
        public Guid IDAccessoryProduct { get; set; }
        public Guid? IDDefineDetailAccessory { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public int? Order { get; set; }
        public int? Status { get; set; }
    }
}