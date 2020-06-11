using System;

namespace SCMCore.Model
{
    public interface IRelatePurchaseOrderFile
    {
        Guid? IDRelatePurchaseOrderFile { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        Guid? IDPurchaseOrderFile { get; set; }
        Guid? IDPurchaseOrder { get; set; }
        Guid? IDRegister { get; set; }
        DateTime? CreateDate { get; set; }
        string HsCode { get; set; }
        string IDCode { get; set; }
        decimal UnderValue { get; set; }


        Guid? IDSupplier { get; set; }
    }
}