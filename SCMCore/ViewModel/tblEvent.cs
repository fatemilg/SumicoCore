using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblEvent : Model.IEvent
    {
        public Guid? IDEvent { get; set; }
        public Guid? IDParent { get; set; }
        public Guid? IDMenu { get; set; }
        public string Titel_Fa { get; set; }
        public string Title_En { get; set; }
        public int? Status { get; set; }

    }
}