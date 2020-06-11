using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCompany : Model.ICompany
    {
        public Guid? IDCompany { get; set; }
        public Guid? IDParent { get; set; }
        public string Name_Fa { get; set; }
    }
}