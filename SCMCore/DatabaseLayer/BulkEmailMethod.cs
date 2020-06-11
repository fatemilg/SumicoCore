using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    class BulkEmailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetSentEmailData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblBulkEmail_GetData", search);
        }

        public bool Unsubscribe_To_True(ViewModel.tblBulkEmail tblBulkEmail)
        {
            return (sqlHelper.RunProcedure("sp_tblBulkEmail_Update_Unsubscribe_To_True", tblBulkEmail) > 0);
        }
    }
}
