using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class FormQuestionMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetFormQuestionDataByIDDynamicForm(ViewModel.tblFormQuestion tblFormQuestion)
        {
            return sqlHelper.ReturnJsonData("sp_tblFormQuestion_GetDataByIDDynamicForm", tblFormQuestion);
        }
        public bool AddFormQuestion(ViewModel.tblFormQuestion tblFormQuestion)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestion_Insert", tblFormQuestion) > 0);
        }
        public bool UpdateFormQuestion(ViewModel.tblFormQuestion tblFormQuestion)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestion_Update", tblFormQuestion) > 0);
        }
        public bool DeleteFormQuestion(ViewModel.tblFormQuestion tblFormQuestion)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestion_Delete", tblFormQuestion) > 0);
        }
        public bool ChangeSortQuestions(ViewModel.tblFormQuestion tblFormQuestion)
        {
            return (sqlHelper.RunProcedure("sp_tblFormQuestion_ChangeSort", tblFormQuestion) > 0);
        }
    }
}






