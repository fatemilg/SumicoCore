using System;

namespace SCMCore.Model
{
    public interface IQuestionCustomer
    {
        Guid? IDQuestionCustomer { get; set; }
        Guid? IDParentOption { get; set; }
        Guid? IDParentQuestion { get; set; }

        string Question_Fa { get; set; }
        string ShortTitle { get; set; }
        bool? UseInCombination { get; set; }
        bool? UseInSignUp { get; set; }
        int? sort { get; set; }
    }
}