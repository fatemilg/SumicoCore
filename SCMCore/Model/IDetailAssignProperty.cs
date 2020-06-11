using System;

namespace SCMCore.Model
{
    public interface IDetailAssignProperty
    {
         Guid? IDDetailAssignProperty { get; set; }
         Guid? IDRet { get; set; }
         Guid? IDProperty { get; set; }
         int? Sort { get; set; }
    }
}