using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IContentDictionary
    {
        Guid? IDContentDictionary { get; set; }
        Guid? IDContent { get; set; }
        Guid? IDDictionary { get; set; }
        int? Sort { get; set; }
    }
}
