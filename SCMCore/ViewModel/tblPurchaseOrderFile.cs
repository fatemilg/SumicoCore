using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPurchaseOrderFile : Model.IPurchaseOrderFile
    {
        public Guid? IDPurchaseOrderFile { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDCurrency { get; set; }
        public string Title { get; set; }
        public string DeliverDate { get; set; }
        public string FileUrl { get; set; }
        public Int64? FileSize { get; set; }
        public bool? Show { get; set; }

        public string JsonPartNumberInFile { get; set; }
        public string ExcelJson { get; set; }
        public string JsonPurchaseOrderFile { get; set; }

    }
}