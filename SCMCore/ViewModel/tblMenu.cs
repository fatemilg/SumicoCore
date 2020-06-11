using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblMenu : Model.IMenu
    {
        public Guid IDMenu { get; set; }
        public Guid? ParentID { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public int? Order { get; set; }
        public bool? Active { get; set; }
        public int? Status { get; set; }
        public string MenuUrl { get; set; }
        public string PicUrl { get; set; }

    }
}