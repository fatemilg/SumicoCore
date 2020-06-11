using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class FormQuestionTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetFormQuestionTypeJsonData(ViewModel.tblFormQuestionType tblFormQuestionType)
        {
            return sqlHelper.ReturnJsonData("sp_tblFormQuestionType_GetData", tblFormQuestionType);
        }
        public bool AddFormQuestionType(ViewModel.tblFormQuestionType tblFormQuestionType)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestionType_Insert", tblFormQuestionType) > 0);
        }
        public bool UpdateFormQuestionType(ViewModel.tblFormQuestionType tblFormQuestionType)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestionType_Update", tblFormQuestionType) > 0);
        }
        public bool DeleteFormQuestionType(ViewModel.tblFormQuestionType tblFormQuestionType)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestionType_Delete", tblFormQuestionType) > 0);
        }
    }
}






