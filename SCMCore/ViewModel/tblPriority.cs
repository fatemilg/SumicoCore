using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPriority : Model.IPriority
    {
        public Guid IDPriority { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public int? Status { get; set; }
    }
}