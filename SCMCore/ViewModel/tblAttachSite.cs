using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblAttachSite : Model.IAttachSite
    {
        public Guid? IDAttachSite { get; set; }
        public string Name_Fa { get; set; }
        public string Description_Fa { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }


        public int? Order { get; set; }//tblAttachCrmInterFace 
        public Guid? IDRet { get; set; } //tblAttachCrmInterFace 
        public Guid? IDUser { get; set; } //tblAttachCrmInterFace 
        public Guid? IDAttachCrmInterface { get; set; } //tblAttachCrmInterFace 
        public Guid? IDAttachInterfaceCategory { get; set; } //tblAttachCrmInterFace 
        public DateTime? CreateDate { get; set; } //tblAttachCrmInterFace 
    }
}