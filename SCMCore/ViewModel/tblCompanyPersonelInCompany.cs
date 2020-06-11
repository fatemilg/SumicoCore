using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCompanyPersonelInCompany : Model.ICompanyPersonelInCompany
    {
        public Guid? IDCompanyPersonelInCompany { get; set; }
        public Guid? IDCompany { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? IDPersonelInCompany { get; set; }
        public Guid? IDLevelOfPersonelInCompany { get; set; }
        public Guid? IDOrganizationalPosition { get; set; }
        public Guid? IDIntroducer { get; set; }
        public int Status { get; set; }

    }
}