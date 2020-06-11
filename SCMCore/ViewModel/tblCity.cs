using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblCity : Model.ICity
    {
        public Guid IDCity { get; set; }
        public Guid? IDState { get; set; }
        public string Name_Fa { get; set; }
        public int? status { get; set; }
    }
}