using System;

namespace SCMCore.Model
{
    public interface IRulePropertyProduct
    {
        Guid? IDRulePropertyProduct { get; set; }
        Guid? IDProperty { get; set; }
        Guid? IDProduct { get; set; }
        Guid? IDDefineDetailProduct { get; set; }
        Guid? IDNextParent { get; set; }
        string FilterItems { get; set; }
    }
}