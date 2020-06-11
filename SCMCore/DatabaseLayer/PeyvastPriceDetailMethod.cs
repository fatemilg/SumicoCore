using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class PeyvastPriceDetailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetPeyvastPriceDetail(ViewModel.tblPeyvastPriceDetail PeyvastPriceDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastPriceDetail_GetPeyvastPriceDetail", PeyvastPriceDetail);
        }
        public JArray GetSumsColumnsOfPeyvastPriceDetail(ViewModel.tblPeyvastPriceDetail PeyvastPriceDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblPeyvastPriceDetail_GetSumsColumns", PeyvastPriceDetail);
        }
   
    }
}