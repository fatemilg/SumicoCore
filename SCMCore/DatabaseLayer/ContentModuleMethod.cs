using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class ContentModuleMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContentModuleJsonData(ViewModel.tblContentModule ContentModule)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentModule_GetData", ContentModule);
        }
        
        public bool AddContentModule(ViewModel.tblContentModule tblContentModule)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModule_Insert", tblContentModule) > 0);
        }
        public bool UpdateContentModule(ViewModel.tblContentModule tblContentModule)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModule_Update", tblContentModule) > 0);
        }
        public bool DeleteContentModule(ViewModel.tblContentModule tblContentModule)
        {
            return (sqlHelper.RunProcedure("sp_tblContentModule_Delete", tblContentModule) > 0);
        }
    }
}






