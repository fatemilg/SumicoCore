using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAccessLevel:Model.IAccessLevel
    {
        public Guid? IDAccessLevel { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDMenu { get; set; }
        public Guid? IDRole { get; set; }
        //public bool Add { get; set; }
        //public bool Update { get; set; }
        //public bool Delete { get; set; }
        public int? Status { get; set; }
        public string MenuUrl { get; set; }
        public string Title_En { get; set; }
        public bool? Access { get; set; }

        // field add,delete ,update dar jadvale Accesslevel mojood hast vali inja nayavordim,tooie procedure marboot be insert va update bayad be soorate defualt null rad shavad
    }
}