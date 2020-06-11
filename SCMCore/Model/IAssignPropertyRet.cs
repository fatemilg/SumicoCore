using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IAssignPropertyRet
    {
        Guid? IDAssignPropertyRet { get; set; }
        Guid? IDRet { get; set; }
        Guid? IDProperty { get; set; }
    }
}