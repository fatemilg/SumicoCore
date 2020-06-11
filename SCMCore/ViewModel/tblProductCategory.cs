using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblProductCategory : Model.IProductCategory
    {
        public Guid? IDProductCategory { get; set; }
        public Guid? ParentID { get; set; }
        public Guid? IDSupplier { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string PicUrl { get; set; }
        public int? Order { get; set; }
        public string MetaTags { get; set; }
        public string MetaDescriptions { get; set; }
        public bool? ShowInSite { get; set; }
        public int? Status { get; set; }
        public int? IDXProductCategory { get; set; }
        public int? IDXParentCategory { get; set; }
        public int? IDXSupplier { get; set; }

    }
}