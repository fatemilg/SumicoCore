using System;
namespace SCMCore.ViewModel
{
    public class tblGalleryCategory : Model.IGalleryCategory
    {
        public Guid? IDGalleryCategory { get; set; }
        public string Title { get; set; }
        public Guid? IDParent { get; set; }
    }
}






