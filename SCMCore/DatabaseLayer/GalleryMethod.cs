using SCMCore.Classes;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class GalleryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetGalleryJsonDataForContent(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblGallery_GetDataForContent", search);
        }
        public JArray GetGalleryJsonDataForTrainingCourse(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblGallery_GetDataForTrainingCourse", search);
        }
        public JArray GetGalleryJsonDataForTrainingCourseUser(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblGallery_GetDataForTrainingCourseUser", search);
        }
        public JArray GetGalleryJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblGallery_GetData", search);
        }
        public bool AddGallery(ViewModel.tblGallery Gallery)
        {
            return (sqlHelper.RunProcedure("sp_tblGallery_Insert", Gallery) > 0);
        }

        public bool UpdateGallery(ViewModel.tblGallery Gallery)
        {
            return (sqlHelper.RunProcedure("sp_tblGallery_Update", Gallery) > 0);
        }

        public bool DeleteGallery(ViewModel.tblGallery Gallery)
        {
            return (sqlHelper.RunProcedure("sp_tblGallery_DeleteRow", Gallery) > 0);
        }
    }
}