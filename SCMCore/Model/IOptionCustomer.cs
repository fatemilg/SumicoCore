using System;

namespace SCMCore.Model
{
    public interface IOptionCustomer
    {
        Guid? IDOptionCustomer { get; set; }
        Guid? IDQuestionCustomer { get; set; }
        string Option_Fa { get; set; }
        int? Sort { get; set; }
    }
}