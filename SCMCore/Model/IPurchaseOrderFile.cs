using System;

namespace SCMCore.Model
{
    public interface IPurchaseOrderFile
    {
        Guid? IDPurchaseOrderFile { get; set; }
        Guid? IDPersonel { get; set; }
        Guid? IDSupplier { get; set; }
        Guid? IDCurrency { get; set; }
        string Title { get; set; }
        string DeliverDate { get; set; }
        string FileUrl { get; set; }
        Int64? FileSize { get; set; }
        bool? Show { get; set; }

        string JsonPartNumberInFile { get; set; }
        string ExcelJson { get; set; }
        string JsonPurchaseOrderFile { get; set; }

    }
}