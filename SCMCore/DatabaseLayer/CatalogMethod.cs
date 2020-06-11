using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class CatalogMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetCatalogData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblCatalog_GetData", search);
        }
        public JArray GetJsonCatalogData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblCatalog_GetData", search);
        }
        public bool AddCatalog(ViewModel.tblCatalog Catalog)
        {
            return (sqlHelper.RunProcedure("sp_tblCatalog_Insert", Catalog) > 0);
        }

        public bool UpdateCatalog(ViewModel.tblCatalog Catalog)
        {
            return (sqlHelper.RunProcedure("sp_tblCatalog_Update", Catalog) > 0);
        }
        public bool DeleteCatalog(ViewModel.tblCatalog Catalog)
        {
            return (sqlHelper.RunProcedure("sp_tblCatalog_DeleteRow", Catalog) > 0);
        }
    }
}