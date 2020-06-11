using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IFormQuestion
    {
        Guid? IDFormQuestion { get; set; }
        Guid? IDDynamicForm { get; set; }
        Guid? IDFormQuestionType { get; set; }
        string QuestionText { get; set; }
        int? Sort { get; set; }
        bool? Optional { get; set; }
    }
}
