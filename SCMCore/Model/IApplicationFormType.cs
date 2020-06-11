using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IApplicationFormType
    {
        Guid? IDApplicationFormType { get; set; }
        string Title { get; set; }
        int? Sort { get; set; }


    }
}