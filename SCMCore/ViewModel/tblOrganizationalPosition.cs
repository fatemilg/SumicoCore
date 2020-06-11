using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblOrganizationalPosition : Model.IOrganizationalPosition
    {
        public Guid IDOrganizationPosition { get; set; }
        public string Name_Fa { get; set; }
        public Guid? ParentID { get; set; }
        public int? Status { get; set; }

    }
}