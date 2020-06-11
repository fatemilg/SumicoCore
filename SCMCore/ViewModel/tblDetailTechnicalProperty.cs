using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblDetailTechnicalProperty : Model.IDetailTechnicalProperty
    {
        public Guid IDDetailTechnicalProperty { get; set; }
        public Guid? IDTechnicalProperty { get; set; }
        public Guid? IDUser { get; set; }
        public Guid? IDRet { get; set; }
        public string Value_Fa { get; set; }
        public string Value_En { get; set; }
        public int? Status { get; set; }
        public int? Order { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}