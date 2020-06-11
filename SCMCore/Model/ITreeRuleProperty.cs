using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface ITreeRuleProperty
    {
        Guid? IDTreeRuleProperty { get; set; }
        Guid? IDProduct { get; set; }
        string GeneratedTree { get; set; }
    }
}
