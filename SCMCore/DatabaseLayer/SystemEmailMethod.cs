using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class SystemEmailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetSystemEmailJsonData(ViewModel.tblSystemEmail SystemEmail)
        {
            return sqlHelper.ReturnJsonData("sp_tblSystemEmail_GetData_json", SystemEmail);
        }
        public DataSet GetSystemEmailDataSetData(ViewModel.tblSystemEmail SystemEmail)
        {
            return sqlHelper.returnDataSet("sp_tblSystemEmail_GetData_ds", SystemEmail);
        }
    }
}