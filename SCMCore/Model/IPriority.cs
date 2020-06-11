using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Model
{
    public interface IPriority
    {
         Guid IDPriority { get; set; }
         string Name { get; set; }
         int? Order { get; set; }
         int? Status { get; set; }
    }
}