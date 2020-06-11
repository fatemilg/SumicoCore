using System;

namespace SCMCore.Model
{
    public interface IGroupType
    {
        Guid? IDGroupType { get; set; }
        string Title_Fa { get; set; }
        string UnicName { get; set; }
    }
}