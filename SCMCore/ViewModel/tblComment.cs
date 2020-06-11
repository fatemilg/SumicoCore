using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblComment : Model.IComment
    {
        public Guid? IDComment { get; set; }
        public Guid? SenderID { get; set; }
        public string SenderFName { get; set; }
        public string SenderLName { get; set; }
        public string SenderEmail { get; set; }
        public string Comment { get; set; }
        public string SenderWebSite { get; set; }
        public Guid? IDContent { get; set; }
        public int? IDXContent { get; set; }
        public Guid? ParentCommentID { get; set; }
        public bool? Show { get; set; }
        public int? Status { get; set; }
    }
}