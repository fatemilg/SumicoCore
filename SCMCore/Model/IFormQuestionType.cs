using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IFormQuestionType
    {
        Guid? IDFormQuestionType { get; set; }
        string UniqueName { get; set; }
    }
}
