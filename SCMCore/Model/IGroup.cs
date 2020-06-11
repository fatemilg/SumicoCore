using System;

namespace SCMCore.Model
{
    public interface IGroup
    {
        Guid? IDGroup { get; set; }
        Guid? IDGroupType { get; set; }

        string Title { get; set; }
        int? Order { get; set; }
        Guid? IDParent { get; set; }

    }

}