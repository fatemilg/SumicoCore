using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseContentMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTrainingCourseContentJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseContent_GetData", search);
        }
        public JArray GetTrainingCourseContentJsonData_ByIDTrainingCourse(ViewModel.tblTrainingCourseContent TrainingCourseContent)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentCategoryType_GetCompleteData_ForTrainingCourseContent", TrainingCourseContent);
        }
        public bool AddTrainingCourseContent(ViewModel.tblTrainingCourseContent tblTrainingCourseContent)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseContent_Insert", tblTrainingCourseContent) > 0);
        }
        public bool SaveTrainingCourseContent(ViewModel.tblTrainingCourseContent tblTrainingCourseContent)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseContent_SaveToggle", tblTrainingCourseContent) > 0);
        }
        public bool UpdateTrainingCourseContent(ViewModel.tblTrainingCourseContent tblTrainingCourseContent)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseContent_Update", tblTrainingCourseContent) > 0);
        }
        public bool DeleteTrainingCourseContent(ViewModel.tblTrainingCourseContent tblTrainingCourseContent)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseContent_Delete", tblTrainingCourseContent) > 0);
        }
    }
}






