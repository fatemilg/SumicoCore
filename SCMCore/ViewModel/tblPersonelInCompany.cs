using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPersonelInCompany : Model.IPersonelInCompany
    {
        public Guid? IDPersonelInCompany { get; set; }
        public int? IDX { get; set; }
        public Guid? IDCompany { get; set; }
        public Guid? IDOrganizationalPosition { get; set; }
        public Guid? IDIntroducer { get; set; }
        public Guid? IDLevelOfPersonelInCompany { get; set; }
        public Guid? IDInterfaceSale { get; set; }
        public string FName_Fa { get; set; }
        public string LName_Fa { get; set; }
        public string FName_En { get; set; }
        public string LName_En { get; set; }
        public string NationalCode { get; set; }
        public string PicUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Status { get; set; }
        public bool? Sex { get; set; }
    }
}