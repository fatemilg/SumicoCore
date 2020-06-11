using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblLogUser : Model.ILogUser
    {
        public Guid? IDLogUser { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDRet { get; set; }
        public Guid? IDUserAction { get; set; }
        public Guid? IDTableName { get; set; }
        public string UserAction { get; set; }
        public int? Status { get; set; }
    }
}