using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class ContentModuleRetMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContentModuleRetJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModuleRet_GetData", search);
        }
        public JArray GetContentModuleByIDRetJsonData(ViewModel.tblContentModuleRet ContentModuleRet)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModuleRet_GetData_ByIDRet", ContentModuleRet);
        }
        public JArray GetContentModuleByUniqueNameJsonData(ViewModel.tblContentModuleRet ContentModuleRet)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModuleRet_GetData_ByUniqueName", ContentModuleRet);
        }
        public JArray GetContentModuleByIDContentModule(ViewModel.tblContentModuleRet ContentModuleRet)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModuleRet_GetData_ByIDContentModule", ContentModuleRet);
        }
        public JArray GetContentModuleByIDContentModule_ForTrainingCourseBatch(ViewModel.tblContentModuleRet ContentModuleRet)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModuleRet_GetData_ByIDContentModule_ForTrainingCourseBatch", ContentModuleRet);
        }
        public bool AddContentModuleRet(ViewModel.tblContentModuleRet tblContentModuleRet)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModuleRet_Insert", tblContentModuleRet) > 0);
        }
        public bool UpdateContentModuleRet(ViewModel.tblContentModuleRet tblContentModuleRet)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModuleRet_Update", tblContentModuleRet) > 0);
        }
        public bool UpdateSortContentModuleRet(ViewModel.tblContentModuleRet tblContentModuleRet)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModuleRet_UpdateSort", tblContentModuleRet) > 0);
        }
        public bool DeleteContentModuleRet(ViewModel.tblContentModuleRet tblContentModuleRet)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModuleRet_Delete", tblContentModuleRet) > 0);
        }
    }
}






