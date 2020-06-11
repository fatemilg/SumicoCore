using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class QuestionCustomerMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionCustomer_GetData", tblQuestionCustomer);
        }
        public JArray GetQuestionCustomerForSignUp(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionCustomer_GetDataForSignUp", tblQuestionCustomer);
        }
        public JArray GetQuestionCustomerForMaterialPageFirstStep(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionCustomer_GetDataForMaterialPageFirstStep", tblQuestionCustomer);
        }
        public JArray GetQuestionCustomerForMaterialPageOtherStep(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionCustomer_GetDataForMaterialPageOtherStep", tblQuestionCustomer);
        }
        public JArray CheckSortSequently(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionCustomer_CheckSortSequently", tblQuestionCustomer);
        }
        public bool AddQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_Insert", tblQuestionCustomer) > 0);
        }
        public bool UpdateQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_Update", tblQuestionCustomer) > 0);
        }
        public bool UpdateParentQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_UpdateParent", tblQuestionCustomer) > 0);
        }
        public bool UpdateQuestionCustomerAsRoot(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_UpdateAsRoot", tblQuestionCustomer) > 0);
        }
        public bool UpdateUseInCombination(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_UpdateUseInCombination", tblQuestionCustomer) > 0);
        }
        public bool UpdateUseInSignUp(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_UpdateUseInSignUp", tblQuestionCustomer) > 0);
        }
        public bool UpdateUseInMaterialDetail(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_UpdateUseInMaterialDetail", tblQuestionCustomer) > 0);
        }

        public bool ChangeSortInQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_ChangeSortInQuestionCustomer", tblQuestionCustomer) > 0);
        }
        public bool DeleteQuestionCustomer(ViewModel.tblQuestionCustomer tblQuestionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionCustomer_Delete", tblQuestionCustomer) > 0);
        }
    }
}