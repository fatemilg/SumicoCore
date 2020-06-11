using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblBanner : Model.IBanner
    {
        public Guid? IDBanner { get; set; }
        public Guid? IDRet { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public string PicUrl { get; set; }
        public string PicUrl_Mobile { get; set; }
        public string ForeignLink { get; set; }
        public bool? Active { get; set; }
        public int? Sort { get; set; } 
        public int? Status { get; set; }

    }
}