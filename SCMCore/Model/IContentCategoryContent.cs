using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IContentCategoryContent
    {
        Guid? IDContentCategoryContent { get; set; }
        Guid? IDContentCategory { get; set; }
        Guid? IDContent { get; set; }
        int? Sort { get; set; }
    }
}
