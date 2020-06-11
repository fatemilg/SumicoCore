using System;
namespace SCMCore.ViewModel
{
    public class tblContentModuleRet : Model.IContentModuleRet
    {
        public Guid? IDContentModuleRet { get; set; }
        public Guid? IDContentModule { get; set; }
        public Guid? IDRet { get; set; }
        public int? Sort { get; set; }
        public string UniqueName { get; set; }
        public string JsonContentModuleRet { get; set; }
    }
}






