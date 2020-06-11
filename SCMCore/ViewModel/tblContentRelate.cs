using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblContentRelate : Model.IContentRelate
    {
        public Guid? IDContentRelate { get; set; }
        public Guid? IDSourceContent { get; set; }
        public Guid? IDDestinationContent { get; set; }
        public Guid? IDContent { get; set; }
    }
}