using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseBatchMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTrainingCourseBatchJsonData(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatch_GetData", tblTrainingCourseBatch);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search TrainingCourseBatch)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatch_GetFullJsonDataForSearch", TrainingCourseBatch);
        }
        public JArray GetDataForSiteWithTrainingCourse(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatch_GetDataForSiteWithTrainingCourse", tblTrainingCourseBatch);
        }
        public JArray GetDataForSiteWithTrainingCourse_ByIDXTrainingCourseBatch(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatch_GetDataForSiteWithTrainingCourse_ByIDXTrainingCourseBatch", tblTrainingCourseBatch);
        }
        public bool AddTrainingCourseBatch(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatch_Insert", tblTrainingCourseBatch) > 0);
        }
        public bool UpdateTrainingCourseBatch(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatch_Update", tblTrainingCourseBatch) > 0);
        }
        public bool DeleteTrainingCourseBatch(ViewModel.tblTrainingCourseBatch tblTrainingCourseBatch)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatch_Delete", tblTrainingCourseBatch) > 0);
        }
    }
}






