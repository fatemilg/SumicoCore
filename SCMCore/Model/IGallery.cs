using System;

namespace SCMCore.Model
{
    public interface IGallery
    {
         Guid? IDGallery { get; set; }
         Guid? IDRet { get; set; }
         Guid? IDGalleryCategory { get; set; }
         string Name_Fa { get; set; }
         string Name_En { get; set; }
         string FileType { get; set; }
         int? FileSize { get; set; }
         string Url { get; set; }
         int? Status { get; set; }
    }
}