using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblFormOption : Model.IFormOption
    {
        public Guid? IDFormOption { get; set; }
        public Guid? IDFormQuestion { get; set; }
        public string OptionText { get; set; }
        public string PicUrl { get; set; }
        public string JsonFormOption { get; set; }
        public int? Sort { get; set; }
    }
}