using System;

namespace SCMCore.Model
{
    public interface ICompany
    {
        Guid? IDCompany { get; set; }
        Guid? IDParent { get; set; }
        string Name_Fa { get; set; }
    }
}