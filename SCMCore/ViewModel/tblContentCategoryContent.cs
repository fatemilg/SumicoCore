using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContentCategoryContent : Model.IContentCategoryContent
    {
        public Guid? IDContentCategoryContent { get; set; }
        public Guid? IDContentCategory { get; set; }
        public Guid? IDContent { get; set; }
        public int? Sort { get; set; }
        public string JsonContentCategoryContent { get; set; }
    }
}