using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblDynamicFormRet : Model.IDynamicFormRet
    {
        public Guid? IDDynamicFormRet { get; set; }
        public Guid? IDDynamicForm { get; set; }
        public Guid? IDRet { get; set; }
        public int? IDX { get; set; }
    }
}