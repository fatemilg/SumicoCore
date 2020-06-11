using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPurchaseOrders : Model.IPurchaseOrders
    {
        public Guid IDPurchaseOrder { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }
}