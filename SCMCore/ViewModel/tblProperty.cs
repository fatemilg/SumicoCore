using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblProperty : Model.IProperty
    {
        public Guid IDProperty { get; set; }
        public Guid? IDParent { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string Description_Fa { get; set; }
        public string Description_En { get; set; }
        public int? Sort { get; set; }
        public string PicUrl { get; set; }
        public int? Status { get; set; }
    }
}