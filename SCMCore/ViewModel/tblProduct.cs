using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblProduct : Model.IProduct
    {
        public Guid? IDProduct { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDProductCategory { get; set; }
        public Guid? IDProductFamily { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string MetaTags { get; set; }
        public string MetaDescriptions { get; set; }
        public string ProductUrl { get; set; }
        public bool? Accessory { get; set; }
        public string IndexDescriptionText { get; set; }
        public string IndexDescriptionPicUrl { get; set; }
        public int? Sort { get; set; }
        public int? Status { get; set; }

        ///------------- For Advance Search
        public string strIDProductCategory { get; set; }
        public string strIDProperty { get; set; }
        public string strIDSupplier { get; set; }
        public string VendorName { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
    }
}