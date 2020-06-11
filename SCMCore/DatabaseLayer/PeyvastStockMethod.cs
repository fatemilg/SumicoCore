using Newtonsoft.Json.Linq;
using SCMCore.Classes;


namespace SCMCore.DatabaseLayer
{
    public class PeyvastStockMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetPeyvastStockData(ViewModel.tblPeyvastStock PeyvastStock)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastStock_GetData", PeyvastStock);
        }
    }
}