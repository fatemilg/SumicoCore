using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblPhoneContact : Model.IPhoneContact
    {
        public Guid IDPhoneContact { get; set; }
        public Guid? IDPhone { get; set; }
        public Guid? IDUser { get; set; }
        public int Status { get; set; }
    }
}