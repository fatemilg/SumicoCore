using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTrainingCourseJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourse_GetData", search);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourse_GetFullJsonDataForSearch", search);
        }
        public JArray GetDataForSiteByIDX(ViewModel.tblTrainingCourse tblTrainingCourse)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourse_GetDataForSiteByIDX", tblTrainingCourse);
        }
        public JArray GetDataForSiteByIDXTrainingCourseCategory(ViewModel.tblTrainingCourseCategory tblTrainingCourseCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourse_GetDataByIDXTrainingCourseCategory", tblTrainingCourseCategory);
        }
        public bool AddTrainingCourse(ViewModel.tblTrainingCourse tblTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourse_Insert", tblTrainingCourse) > 0);
        }
        public bool UpdateTrainingCourse(ViewModel.tblTrainingCourse tblTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourse_Update", tblTrainingCourse) > 0);
        }
        public bool DeleteTrainingCourse(ViewModel.tblTrainingCourse tblTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourse_Delete", tblTrainingCourse) > 0);
        }
    }
}






