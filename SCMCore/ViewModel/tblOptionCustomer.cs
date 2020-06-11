using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblOptionCustomer : Model.IOptionCustomer
    {
        public Guid? IDOptionCustomer { get; set; }
        public Guid? IDQuestionCustomer { get; set; }
        public string Option_Fa { get; set; }
        public int? Sort { get; set; }
        public string JsonOptionCustomer { get; set; }
    }
}