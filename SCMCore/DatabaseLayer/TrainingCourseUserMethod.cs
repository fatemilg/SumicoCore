using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseUserMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTrainingCourseUserJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseUser_GetData", search);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseUser_GetFullJsonDataForSearch", search);
        }
        public bool AddTrainingCourseUser(ViewModel.tblTrainingCourseUser tblTrainingCourseUser)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseUser_Insert", tblTrainingCourseUser) > 0);
        }
        public bool UpdateTrainingCourseUser(ViewModel.tblTrainingCourseUser tblTrainingCourseUser)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseUser_Update", tblTrainingCourseUser) > 0);
        }
        public bool DeleteTrainingCourseUser(ViewModel.tblTrainingCourseUser tblTrainingCourseUser)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseUser_Delete", tblTrainingCourseUser) > 0);
        }
    }
}






