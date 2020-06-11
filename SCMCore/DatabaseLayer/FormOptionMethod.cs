using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class FormOptionMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetDataByIDFormOption(ViewModel.tblFormOption tblFormOption)
        {
            return sqlHelper.ReturnJsonData("sp_tblFormOption_GetDataByIDFormOption", tblFormOption);
        }
        public JArray GetFormOptionDataByIDFormQuestion(ViewModel.tblFormOption tblFormOption)
        {
            return sqlHelper.ReturnJsonData("sp_tblFormOption_GetDataByIDFormQuestion", tblFormOption);
        }
        public bool AddFormOption(ViewModel.tblFormOption tblFormOption)
        {
            return (sqlHelper.RunProcedure("sp_tblFormOption_Insert", tblFormOption) > 0);
        }
        public bool ChangeSortOptions(ViewModel.tblFormOption tblFormOption)
        {
            return (sqlHelper.RunProcedure("sp_tblFormOption_ChangeSort", tblFormOption) > 0);
        }
        public bool UpdateFormOption(ViewModel.tblFormOption tblFormOption)
        {
            return (sqlHelper.RunProcedure("sp_tblFormOption_Update", tblFormOption) > 0);
        }
        public bool DeleteFormOption(ViewModel.tblFormOption tblFormOption)
        {
            return (sqlHelper.RunProcedure("sp_tblFormOption_Delete", tblFormOption) > 0);
        }
    }
}






