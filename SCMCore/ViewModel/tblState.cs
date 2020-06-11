using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblState : Model.IState
    {
        public Guid? IDState { get; set; }
        public Guid? IDCountry { get; set; }
        public string Name_Fa { get; set; }
        public int? status { get; set; }
    }
}