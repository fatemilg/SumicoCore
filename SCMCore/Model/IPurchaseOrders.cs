using System;
using System.Web;

namespace SCMCore.Model
{
    public interface IPurchaseOrders
    {
        Guid IDPurchaseOrder { get; set; }
        string Title { get; set; }
        HttpPostedFileBase Attachment { get; set; }
    }
}