using System;

namespace SCMCore.ViewModel
{
    public class tblOptionInheritance : Model.IOptionInheritance
    {
        public Guid? IDOptionInheritance { get; set; }
        public Guid? IDOptionCustomer { get; set; }
        public Guid? IDParent { get; set; }
        public bool? EndNodeByQuestion { get; set; }

        public int? SortMainQuestion { get; set; }

        public int? SortOption { get; set; }
        public string GeneratedSorts { get; set; }

        public Guid? IDSeller { get; set; }



    }
}