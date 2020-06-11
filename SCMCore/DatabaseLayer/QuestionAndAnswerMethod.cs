using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class QuestionAndAnswerMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetQuestionAndAnswerForDefineDetailProductJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblQuestionAndAnswer_GetData_ForDefineDetailProduct", search);
        }
        public bool AddQuestionAndAnswer(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionAndAnswer_Insert", QuestionAndAnswer) > 0);
        }

        public bool UpdateQuestionAndAnswer(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionAndAnswer_Update", QuestionAndAnswer) > 0);
        }
        public bool AcceptByManager(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionAndAnswer_AcceptByManager", QuestionAndAnswer) > 0);
        }
        public bool DenyByManager(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionAndAnswer_DenyByManager", QuestionAndAnswer) > 0);
        }
        public bool DeleteQuestionAndAnswer(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            return (sqlHelper.RunProcedure("sp_tblQuestionAndAnswer_DeleteRow", QuestionAndAnswer) > 0);
        }
    }
}