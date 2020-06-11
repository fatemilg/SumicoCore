using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.DatabaseLayer
{
    public class CurrencyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetCurrencyData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblCurrency_GetData", search);
        }
    }
}