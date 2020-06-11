using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblRole : Model.IRole
    {
        public Guid IDRole { get; set; }
       public string Title { get; set; }
        public int? Status { get; set; }
    }
}