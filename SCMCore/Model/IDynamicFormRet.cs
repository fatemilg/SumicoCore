using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IDynamicFormRet
    {
        Guid? IDDynamicFormRet { get; set; }
        Guid? IDDynamicForm { get; set; }
        Guid? IDRet { get; set; }
        int? IDX { get; set; }
    }
}
