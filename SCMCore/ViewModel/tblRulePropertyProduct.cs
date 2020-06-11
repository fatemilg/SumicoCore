using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblRulePropertyProduct : Model.IRulePropertyProduct
    {
        public Guid? IDRulePropertyProduct { get; set; }
        public Guid? IDProperty { get; set; }
        public Guid? IDProduct { get; set; }
        public Guid? IDDefineDetailProduct { get; set; }
        public Guid? IDNextParent { get; set; }
        public string FilterItems { get; set; }
    }
}