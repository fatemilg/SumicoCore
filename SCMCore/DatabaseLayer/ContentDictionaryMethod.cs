using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class ContentDictionaryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContentDictionaryByIDContentJsonData(ViewModel.tblContentDictionary ContentDictionary)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentDictionary_GetDataByIDContent", ContentDictionary);
        }
        public JArray GetSelectedDataByIDContent(ViewModel.tblContentDictionary ContentDictionary)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentDictionary_GetSelectedDataByIDContent", ContentDictionary);
        }
        public bool AddContentDictionary(ViewModel.tblContentDictionary tblContentDictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblContentDictionary_Insert", tblContentDictionary) > 0);
        }
        public bool UpdateContentDictionary(ViewModel.tblContentDictionary tblContentDictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblContentDictionary_Update", tblContentDictionary) > 0);
        }
        public bool ChangeSortInContentDictionary(ViewModel.tblContentDictionary tblContentDictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblContentDictionary_ChangeSortInContentDictionary", tblContentDictionary) > 0);
        }
        public bool DeleteContentDictionary(ViewModel.tblContentDictionary tblContentDictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblContentDictionary_Delete", tblContentDictionary) > 0);
        }
    }
}






