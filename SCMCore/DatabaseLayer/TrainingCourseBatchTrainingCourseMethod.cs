using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseBatchTrainingCourseMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetDataByIDTrainingCourseBatchByIDTrainingCourseBatch(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatchTrainingCourse_GetDataByIDTrainingCourseBatch", tblTrainingCourseBatchTrainingCourse);
        }
        public JArray GetTrainingCourseBatchTainingCourseByIDTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseBatchTrainingCourse_GetDataByIDTrainingCourse", tblTrainingCourseBatchTrainingCourse);
        }
        public bool AddTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatchTrainingCourse_Insert", tblTrainingCourseBatchTrainingCourse) > 0);
        }
        public bool ToggleSelectTrainingCourseBatch(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatchTrainingCourse_ToggleSelectTrainingCourseBatch", tblTrainingCourseBatchTrainingCourse) > 0);
        }
        public bool UpdateTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatchTrainingCourse_Update", tblTrainingCourseBatchTrainingCourse) > 0);
        }
        public bool ChangeSortTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatchTrainingCourse_UpdateSort", tblTrainingCourseBatchTrainingCourse) > 0);
        }
        public bool DeleteTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse tblTrainingCourseBatchTrainingCourse)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseBatchTrainingCourse_Delete", tblTrainingCourseBatchTrainingCourse) > 0);
        }
    }
}






