using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class SupplierPriceListDetailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetSupplierPriceListDetail(ViewModel.tblSupplierPriceListDetail SupplierPriceListDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblSupplierPriceListDetail_GetSupplierPriceListDetail", SupplierPriceListDetail);
        }
        public JArray GetSumsColumnsOfSupplierPriceListDetail(ViewModel.tblSupplierPriceListDetail SupplierPriceListDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblSupplierPriceListDetail_GetSumsColumns", SupplierPriceListDetail);
        }
    }
}