using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblUnit : Model.IUnit
    {
        public Guid IDUnit { get; set; }
        public string Name_Fa { get; set; }
        public string Description_Fa { get; set; }
        public int? Status { get; set; }
    }
}