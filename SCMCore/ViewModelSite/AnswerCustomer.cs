using System;

namespace SCMCore.ViewModelSite
{
    public class AnswerCustomer : Model.IAnswerCustomer
    {
        public Guid? IDAnswerCustomer { get; set; }
        public Guid? IDOptionCustomer { get; set; }
        public Guid? IDUserSite { get; set; }
    }
}