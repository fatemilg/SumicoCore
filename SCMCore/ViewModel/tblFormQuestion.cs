using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblFormQuestion:Model.IFormQuestion
    {
        public Guid? IDFormQuestion { get; set; }
        public Guid? IDDynamicForm { get; set; }
        public Guid? IDFormQuestionType { get; set; }
        public string QuestionText { get; set; }
        public int? Sort { get; set; }
        public bool? Optional { get; set; }
        public string JsonFormQuestion { get; set; }
    }
}