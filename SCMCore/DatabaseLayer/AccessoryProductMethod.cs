using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class AccessoryProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetAccessoryProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAccessoryProduct_GetData", search);
        }
        public JArray GetAccessoryProductJsonJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblAccessoryProduct_GetData", search);
        }
        public bool AddAccessoryProduct(ViewModel.tblAccessoryProduct Accessory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryProduct_Insert", Accessory) > 0);
        }

        public bool UpdateAccessoryProduct(ViewModel.tblAccessoryProduct Accessory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryProduct_Update", Accessory) > 0);
        }

        public bool DeleteAccessoryProduct(ViewModel.tblAccessoryProduct Accessory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryProduct_DeleteRow", Accessory) > 0);
        }
    }
}