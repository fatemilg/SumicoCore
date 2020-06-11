using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblDictionary : Model.IDictionary
    {
        public Guid? IDDictionary { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Abstract { get; set; }
        public string PicUrl { get; set; }
        public string KeyWord { get; set; }
        public string SourceText { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public int? IDX { get; set; }
        public int? Status { get; set; }
        public bool? Active { get; set; }
    }
}