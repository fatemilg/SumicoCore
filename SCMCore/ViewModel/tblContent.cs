using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContent : Model.IContent
    {
        public Guid? IDContent { get; set; }
        public int? IDX { get; set; }
        public Guid? IDContentCategory { get; set; }
        public Guid? IDPersonel { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string Abstract_Fa { get; set; }
        public string Abstract_En { get; set; }
        public string MetaTags { get; set; }
        public string MetaDescriptions { get; set; }
        public string PicUrl { get; set; }
        public int? Like { get; set; }
        public int? DisLike { get; set; }
        public int? Status { get; set; }
        public bool? Active { get; set; }
    }
}