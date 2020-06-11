using System;

namespace SCMCore.Model
{
    public interface IPurchaseOrder
    {
        Guid? IDPurchaseOrder { get; set; }
        Guid? IDPurchaseOrderFile { get; set; }
        string PartNumber { get; set; }
        string ExcelJson { get; set; }

    }
}