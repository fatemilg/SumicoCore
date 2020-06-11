using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContentDictionary : Model.IContentDictionary
    {
        public Guid? IDContentDictionary { get; set; }
        public Guid? IDContent { get; set; }
        public Guid? IDDictionary { get; set; }
        public int? Sort { get; set; }
        public string JsonContentDictionary { get; set; }
    }
}