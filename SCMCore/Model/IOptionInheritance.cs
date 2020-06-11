using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IOptionInheritance
    {
        Guid? IDOptionInheritance { get; set; }
        Guid? IDOptionCustomer { get; set; }
        Guid? IDParent { get; set; }
        bool? EndNodeByQuestion { get; set; }
        int? SortMainQuestion { get; set; }
        int? SortOption { get; set; }
        string GeneratedSorts { get; set; }



    }
}