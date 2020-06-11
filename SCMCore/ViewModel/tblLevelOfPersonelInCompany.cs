using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblLevelOfPersonelInCompany : Model.ILevelOfPersonelInCompany
    {
        public Guid? IDLevelOfPersonelInCompany { get; set; }
        public string Title { get; set; }
        public int? Sort { get; set; }
        public int? Status { get; set; }
    }
}