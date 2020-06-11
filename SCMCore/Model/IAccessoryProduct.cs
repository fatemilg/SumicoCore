using System;

namespace SCMCore.Model
{
    public interface IAccessoryProduct
    {
        Guid IDAccessoryProduct { get; set; }
        Guid? IDDefineDetailAccessory { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        int? Order { get; set; }
        int? Status { get; set; }
    }
}