using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblQuestionAndAnswer : Model.IQuestionAndAnswer
    {
        public Guid? IDQuestionAndAnswer { get; set; }
        public Guid? IDPersonel { get; set; }
        public Guid? IDRet { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string DescriptionByManager { get; set; }
        public int? Sort { get; set; }
        public bool? Accept { get; set; }
        public string MetaTag { get; set; }
        public int? Status { get; set; }


        public Guid? IDLogUser { get; set; }
        public int? IDXRet { get; set; }


    }
}








