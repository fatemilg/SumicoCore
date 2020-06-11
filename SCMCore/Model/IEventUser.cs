using System;

namespace SCMCore.Model
{
    public interface IEventUser
    {
         Guid? IDEventUser { get; set; }
         Guid? IDUser { get; set; }
         Guid? IDMenu { get; set; }
         Guid? IDEvent { get; set; }
         string EventName { get; set; }
         string Title_Fa { get; set; }
         string Title_En { get; set; }
         string MenuUrl { get; set; }


    }
}