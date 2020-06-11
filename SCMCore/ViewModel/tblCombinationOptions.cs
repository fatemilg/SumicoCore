using System;

namespace SCMCore.ViewModel
{
    public class tblCombinationOptions : Model.ICombinationOptions
    {
        public Guid? IDCombinationOptions { get; set; }
        public Guid? IDOptionInheritance { get; set; }
        public Guid? IDSeller { get; set; }

        public Guid? IDOptionCustomer { get; set; }
    }
}