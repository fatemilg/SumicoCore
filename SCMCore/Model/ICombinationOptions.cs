using System;

namespace SCMCore.Model
{
    public interface ICombinationOptions
    {
        Guid? IDCombinationOptions { get; set; }
        Guid? IDOptionInheritance { get; set; }
        Guid? IDSeller { get; set; }

    }
}