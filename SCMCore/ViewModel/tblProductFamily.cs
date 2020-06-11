using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblProductFamily : Model.IProductFamily
    {
        public Guid IDProductFamily { get; set; }
        public Guid? ParentID { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public int? Status { get; set; }
    }
}