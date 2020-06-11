using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class DynamicFormMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetDynamicFormJsonData(ViewModel.tblDynamicForm DynamicForm)
        {
            return sqlHelper.ReturnJsonData("sp_tblDynamicForm_GetData", DynamicForm);
        }
        public bool AddDynamicForm(ViewModel.tblDynamicForm tblDynamicForm)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicForm_Insert", tblDynamicForm) > 0);
        }
        public bool UpdateDynamicForm(ViewModel.tblDynamicForm tblDynamicForm)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicForm_Update", tblDynamicForm) > 0);
        }
        public bool DeleteDynamicForm(ViewModel.tblDynamicForm tblDynamicForm)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicForm_Delete", tblDynamicForm) > 0);
        }
    }
}






