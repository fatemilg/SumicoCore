using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMCore.Model
{
    interface IFormOption
    {
        Guid? IDFormOption { get; set; }
        Guid? IDFormQuestion { get; set; }
        string OptionText { get; set; }
        string PicUrl { get; set; }
        int? Sort { get; set; }
    }
}
