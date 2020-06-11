using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCurrency : Model.ICurrency
    {
        public Guid IDCurrency { get; set; }
        public string Title { get; set; }

    }
}