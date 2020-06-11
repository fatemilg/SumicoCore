using System;

namespace SCMCore.Model
{
    public interface IEvent
    {
         Guid? IDEvent { get; set; }
         Guid? IDParent { get; set; }
         Guid? IDMenu { get; set; }
         string Titel_Fa { get; set; }
         string Title_En { get; set; }
         int? Status { get; set; }

    }
}