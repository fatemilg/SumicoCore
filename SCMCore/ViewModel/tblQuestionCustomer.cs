using System;

namespace SCMCore.ViewModel
{
    public class tblQuestionCustomer : Model.IQuestionCustomer
    {
        public Guid? IDQuestionCustomer { get; set; }
        public Guid? IDParentOption { get; set; }
        public Guid? IDParentQuestion { get; set; }

        public string Question_Fa { get; set; }
        public string ShortTitle { get; set; }
        public bool? UseInCombination { get; set; }
        public bool? UseInSignUp { get; set; }
        public bool? UseInMaterialDetail { get; set; }
        public int? sort { get; set; }
        public string JsonQuestionCustomer { get; set; }

        public Guid? IDLogUser { get; set; }

        public Guid? IDOptionCustomerSelected { get; set; }
    }
}