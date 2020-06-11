using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class SeparateingFilesMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public DataSet GetAllFiles(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_GetAllFileUrl", search);
        }
    }
}