using System;

namespace SCMCore.ViewModel
{
    public class tblAttachCrm : Model.IAttachCrm
    {
        public Guid? IDAttachCrm { get; set; }
        public string Name_Fa { get; set; }
        public string Description_Fa { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }

        public string AttachCrmCondition { get; set; }   // tamame item haii ke mikhahim dar AttachCrm zakhire shavad be soorat string be in parameter midahim
        public Guid? IDRet { get; set; } //tblAttachCrmInterFace 
    }
}