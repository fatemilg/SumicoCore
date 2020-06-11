using System;

namespace SCMCore.Model
{
    public interface ITechnicalProperty
    {
        Guid IDTechnicalProperty { get; set; }
        string Name_Fa { get; set; }
        string Name_En { get; set; }
        int? Status { get; set; }
    }
}