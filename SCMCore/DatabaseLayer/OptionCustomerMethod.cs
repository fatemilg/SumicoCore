using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class OptionCustomerMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetOptionCustomer(ViewModel.tblOptionCustomer tblOptionCustomer)
        {
            return sqlHelper.ReturnJsonData("sp_tblOptionCustomer_GetData", tblOptionCustomer);
        }
        public bool AddOptionCustomer(ViewModel.tblOptionCustomer tblOptionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblOptionCustomer_Insert", tblOptionCustomer) > 0);
        }
        public bool UpdateOptionCustomer(ViewModel.tblOptionCustomer tblOptionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblOptionCustomer_Update", tblOptionCustomer) > 0);
        }
        public bool ChangeSortInOptionCustomer(ViewModel.tblOptionCustomer tblOptionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblOptionCustomer_ChangeSortInOptionCustomer", tblOptionCustomer) > 0);
        }
        public bool DeleteOptionCustomer(ViewModel.tblOptionCustomer tblOptionCustomer)
        {
            return (sqlHelper.RunProcedure("sp_tblOptionCustomer_Delete", tblOptionCustomer) > 0);
        }
    }
}