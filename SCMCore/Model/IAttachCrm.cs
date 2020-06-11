using System;

namespace SCMCore.Model
{
    public interface IAttachCrm
    {
        Guid? IDAttachCrm { get; set; }
        string Name_Fa { get; set; }
        string Description_Fa { get; set; }
        string FileType { get; set; }
        string FileName { get; set; }
        string Url { get; set; }
        int? Status { get; set; }

        string AttachCrmCondition { get; set; }   // tamame item haii ke mikhahim dar AttachCrm zakhire shavad be soorat string be in parameter midahim
        Guid? IDRet { get; set; } //tblAttachCrmInterFace 
    }
}