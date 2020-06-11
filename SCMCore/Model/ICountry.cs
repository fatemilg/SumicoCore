using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface ICountry
    {
        Guid IDCountry { get; set; }
        string Name_Fa { get; set; }
        int? Status { get; set; }
    }
}