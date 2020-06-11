using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContentCategory : Model.IContentCategory
    {
        public Guid IDContentCategory { get; set; }
        public Guid? IDContentCategoryType { get; set; }
        public Guid? IDParent { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public int? Sort { get; set; }
        public int? Status { get; set; }
    }
}