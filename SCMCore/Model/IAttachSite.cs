using System;

namespace SCMCore.Model
{
    public interface IAttachSite
    {
        Guid? IDAttachSite { get; set; }
        string Name_Fa { get; set; }
        string Description_Fa { get; set; }
        string FileType { get; set; }
        string FileName { get; set; }
        string Url { get; set; }
        int? Status { get; set; }


        int? Order { get; set; }//tblAttachCrmInterFace 
        Guid? IDRet { get; set; } //tblAttachCrmInterFace 
        Guid? IDUser { get; set; } //tblAttachCrmInterFace 
        Guid? IDAttachCrmInterface { get; set; } //tblAttachCrmInterFace 
        Guid? IDAttachInterfaceCategory { get; set; } //tblAttachCrmInterFace 
        DateTime? CreateDate { get; set; } //tblAttachCrmInterFace 
    }
}