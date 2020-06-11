using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblRoleMenu : Model.IRoleMenu
    {
        public Guid IDRoleMenu { get; set; }
        public Guid? IDMenu { get; set; }
        public Guid? IDRole { get; set; }
        public bool? Access { get; set; }
    }
}