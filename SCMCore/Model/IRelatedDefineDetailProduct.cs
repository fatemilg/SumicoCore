using System;

namespace SCMCore.Model
{
    public interface IRelatedDefineDetailProduct
    {
        Guid? IDRelatedDefineDetailProduct { get; set; }
        Guid? IDSourceDefineDetailProduct { get; set; }
        Guid? IDDestinationDefineDetailProduct { get; set; }
        Guid? IDDefineDetailProduct { get; set; } //For Search
        int? IDXDefineDetailProduct { get; set; } //For Search

    }
}