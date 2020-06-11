using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IDynamicForm
    {
        Guid? IDDynamicForm { get; set; }
        string Name_Fa { get; set; }
        string Description { get; set; }
        int? IDX { get; set; }
    }
}
