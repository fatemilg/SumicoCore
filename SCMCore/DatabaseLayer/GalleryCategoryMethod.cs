using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class GalleryCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetGalleryCategoryJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblGalleryCategory_GetData", search);
        }
        public bool AddGalleryCategory(ViewModel.tblGalleryCategory tblGalleryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblGalleryCategory_Insert", tblGalleryCategory) > 0);
        }
        public bool UpdateGalleryCategory(ViewModel.tblGalleryCategory tblGalleryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblGalleryCategory_Update", tblGalleryCategory) > 0);
        }
        public bool DeleteGalleryCategory(ViewModel.tblGalleryCategory tblGalleryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblGalleryCategory_Delete", tblGalleryCategory) > 0);
        }
    }
}






