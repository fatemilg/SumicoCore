using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblProductDefineDetailProduct :Model.IProductDefineDetailProduct
    {
        public Guid? IDProductDefineDetailProduct { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public Guid? IDRet { get; set; }
    }
}