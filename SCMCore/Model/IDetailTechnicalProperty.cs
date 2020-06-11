using System;

namespace SCMCore.Model
{
    public interface IDetailTechnicalProperty
    {
         Guid IDDetailTechnicalProperty { get; set; }
         Guid? IDTechnicalProperty { get; set; }
         Guid? IDUser { get; set; }
         Guid? IDRet { get; set; }
         string Value_Fa { get; set; }
         string Value_En { get; set; }
         int? Status { get; set; }
         int? Order { get; set; }
         DateTime? CreateDate { get; set; }
    }
}