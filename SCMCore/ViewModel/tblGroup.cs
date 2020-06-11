using System;

namespace SCMCore.ViewModel
{
    public class tblGroup : Model.IGroup
    {
        public Guid? IDGroup { get; set; }
        public Guid? IDGroupType { get; set; }

        public string Title { get; set; }
        public int? Order { get; set; }
        public Guid? IDParent { get; set; }

    }

}