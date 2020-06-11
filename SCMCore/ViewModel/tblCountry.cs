using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCountry : Model.ICountry
    {
        public Guid IDCountry { get; set; }
        public string Name_Fa { get; set; }
        public int? Status { get; set; }
    }
}