using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class DynamicFormRetMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetDynamicFormRetJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDynamicFormRet_GetData", search);
        }
        public JArray GetAllDataForSiteByIDX(ViewModel.tblDynamicFormRet tblDynamicFormRet)
        {
            return sqlHelper.ReturnJsonData("sp_tblDynamicFormRet_GetAllDataForSiteByIDX", tblDynamicFormRet);
        }
        public bool AddDynamicFormRet(ViewModel.tblDynamicFormRet tblDynamicFormRet)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicFormRet_Insert", tblDynamicFormRet) > 0);
        }
        public bool UpdateDynamicFormRet(ViewModel.tblDynamicFormRet tblDynamicFormRet)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicFormRet_Update", tblDynamicFormRet) > 0);
        }
        public bool DeleteDynamicFormRet(ViewModel.tblDynamicFormRet tblDynamicFormRet)
        {
            return (sqlHelper.RunProcedure("sp_tblDynamicFormRet_Delete", tblDynamicFormRet) > 0);
        }
    }
}






