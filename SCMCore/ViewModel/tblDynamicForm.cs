using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblDynamicForm : Model.IDynamicForm
    {
        public Guid? IDDynamicForm { get; set; }
        public string Name_Fa { get; set; }
        public string Description { get; set; }
        public int? IDX { get; set; }
    }
}