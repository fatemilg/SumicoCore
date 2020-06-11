using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblEventUser : Model.IEventUser
    {
        public Guid? IDEventUser { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDMenu { get; set; }
        public Guid? IDEvent { get; set; }
        public string EventName { get; set; }
        public string Title_Fa { get; set; }
        public string Title_En { get; set; }
        public string MenuUrl { get; set; }


    }
}