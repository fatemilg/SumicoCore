using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblTreeRuleProperty : Model.ITreeRuleProperty
    {
        public Guid? IDTreeRuleProperty { get; set; }
        public Guid? IDProduct { get; set; }
        public int? IDXProduct { get; set; }
        public string GeneratedTree { get; set; }
    }
}