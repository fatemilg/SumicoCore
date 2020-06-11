using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.ViewModel
{
    public class tblGallery : Model.IGallery
    {
        public Guid? IDGallery { get; set; }
        public Guid? IDRet { get; set; }
        public Guid? IDGalleryCategory { get; set; }
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string FileType { get; set; }
        public int? FileSize { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }
    }
}