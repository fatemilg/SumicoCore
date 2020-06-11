using System;

namespace SCMCore.Model
{
    public interface IUnit
    {
        Guid IDUnit { get; set; }
        string Name_Fa { get; set; }
        string Description_Fa { get; set; }
        int? Status { get; set; }
    }
}