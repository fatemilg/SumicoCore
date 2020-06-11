using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContactWayType : Model.IContactWayType
    {
        public Guid? IDContactWayType { get; set; }
        public string Name_Fa { get; set; }
        public int? Status { get; set; }
    }
}