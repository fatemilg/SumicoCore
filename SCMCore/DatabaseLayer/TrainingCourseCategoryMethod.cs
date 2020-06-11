
using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class TrainingCourseCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetTrainingCourseCategoryJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseCategory_GetData", search);
        }
        public JArray GetTrainingCourseCategoryJsonNestedData(ViewModel.tblTrainingCourseCategory TrainingCourseCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseCategory_GetNestedData", TrainingCourseCategory);
        }
        public JArray GetTrainingCourseCategoryData_Tree(ViewModel.tblTrainingCourseCategory TrainingCourseCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblTrainingCourseCategory_GetData_Tree", TrainingCourseCategory);
        }
        public bool AddTrainingCourseCategory(ViewModel.tblTrainingCourseCategory tblTrainingCourseCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseCategory_Insert", tblTrainingCourseCategory) > 0);
        }
        public bool UpdateTrainingCourseCategory(ViewModel.tblTrainingCourseCategory tblTrainingCourseCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseCategory_Update", tblTrainingCourseCategory) > 0);
        }
        public bool DeleteTrainingCourseCategory(ViewModel.tblTrainingCourseCategory tblTrainingCourseCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblTrainingCourseCategory_Delete", tblTrainingCourseCategory) > 0);
        }
    }
}






