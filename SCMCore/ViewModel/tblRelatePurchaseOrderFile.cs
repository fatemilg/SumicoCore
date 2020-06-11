using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblRelatePurchaseOrderFile : Model.IRelatePurchaseOrderFile
    {
        public Guid? IDRelatePurchaseOrderFile { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public Guid? IDPurchaseOrderFile { get; set; }
        public Guid? IDPurchaseOrder { get; set; }
        public Guid? IDRegister { get; set; }
        public DateTime? CreateDate { get; set; }
        public string HsCode { get; set; }
        public string IDCode { get; set; }
        public decimal UnderValue { get; set; }


        public Guid? IDSupplier { get; set; }
    }
}