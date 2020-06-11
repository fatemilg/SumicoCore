using System;

namespace SCMCore.Model
{
    public interface ILogUser
    {
         Guid? IDLogUser { get; set; }
         Guid? IDUser { get; set; }
         Guid? IDRet { get; set; }
         Guid? IDUserAction { get; set; }
         Guid? IDTableName { get; set; }
         string UserAction { get; set; }
         int? Status { get; set; }
    }
}