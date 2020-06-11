using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAccessoryCategory: Model.IAccessoryCategory
    {
        public Guid IDAccessoryCategory { get; set; }
        public Guid? IDSupplier { get; set; }
        public Guid? IDParent { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public int? Sort { get; set; }
        public int? Status { get; set; }
    }
}