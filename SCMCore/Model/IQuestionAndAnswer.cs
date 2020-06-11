using System;

namespace SCMCore.Model
{
    public interface IQuestionAndAnswer
    {
        Guid? IDQuestionAndAnswer { get; set; }
        Guid? IDRet { get; set; }
        int? IDXRet { get; set; }
        string Question { get; set; }
        string Answer { get; set; }
        int? Sort { get; set; }
        bool? Accept { get; set; }
        string MetaTag { get; set; }

        int? Status { get; set; }
    }
}








