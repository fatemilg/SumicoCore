using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAssignPropertyRet :Model.IAssignPropertyRet
    {
        public Guid? IDAssignPropertyRet { get; set; }
        public Guid? IDRet { get; set; }
        public Guid? IDProperty { get; set; }


    }
}