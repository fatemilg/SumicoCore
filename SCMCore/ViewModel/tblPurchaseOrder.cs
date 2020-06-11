using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPurchaseOrder : Model.IPurchaseOrder
    {
        public Guid? IDPurchaseOrder { get; set; }
        public Guid? IDPurchaseOrderFile { get; set; }
        public string PartNumber { get; set; }
        public string ExcelJson { get; set; }

    }
}