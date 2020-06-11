using System;

namespace SCMCore.Model
{
    public interface IProductFamily
    {
        Guid IDProductFamily { get; set; }
        Guid? ParentID { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        string Description_Fa { get; set; }
        string Description_En { get; set; }
        int? Status { get; set; }
    }
}