using System;

namespace SCMCore.Model
{
    public interface IPhoneContact
    {
         Guid IDPhoneContact { get; set; }
         Guid? IDPhone { get; set; }
         Guid? IDUser { get; set; }
         int Status { get; set; }
    }
}