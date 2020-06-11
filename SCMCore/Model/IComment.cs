using System;

namespace SCMCore.Model
{
    public interface IComment
    {
        Guid? IDComment { get; set; }
        Guid? SenderID { get; set; }
        string SenderFName { get; set; }
        string SenderLName { get; set; }
        string SenderEmail { get; set; }
        string Comment { get; set; }
        string SenderWebSite { get; set; }
        Guid? IDContent { get; set; }
        int? IDXContent { get; set; }
        Guid? ParentCommentID { get; set; }
        bool? Show { get; set; }
        int? Status { get; set; }
    }
}