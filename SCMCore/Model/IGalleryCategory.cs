using System;
namespace SCMCore.Model
{
    public interface IGalleryCategory
    {
         Guid? IDGalleryCategory { get; set; }
         string Title { get; set; }
         Guid? IDParent { get; set; }
    }
}






