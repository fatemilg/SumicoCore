using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblFormQuestionType : Model.IFormQuestionType
    {
        public Guid? IDFormQuestionType { get; set; }
        public string UniqueName { get; set; }
    }
}