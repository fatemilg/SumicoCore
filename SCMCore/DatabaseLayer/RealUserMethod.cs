using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class RealUserMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        //Customer
        public DataSet GetRealUserCustomerData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRealUser_GetCustomerData", search);
        }
        public bool AddRealCustomer(ViewModel.tblRealUser RealUser)
        {
            return (sqlHelper.RunProcedure("sp_tblRealUser_InsertCustomer", RealUser,true) > 0);
        }
        public bool UpdateRealCustomer(ViewModel.tblRealUser RealUser)
        {
            return (sqlHelper.RunProcedure("sp_tblRealUser_UpdateCustomer", RealUser, true) > 0);
        }
        //Supplier
        public DataSet GetRealUserSupplierData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRealUser_GetSupplierData", search);
        }
        public bool AddRealSupplier(ViewModel.tblRealUser RealUser)
        {
            return (sqlHelper.RunProcedure("sp_tblRealUser_InsertSupplier", RealUser, true) > 0);
        }
        public bool UpdateRealSupplier(ViewModel.tblRealUser RealUser)
        {
            return (sqlHelper.RunProcedure("sp_tblRealUser_UpdateSupplier", RealUser, true) > 0);
        }

        public bool DeleteRealUser(ViewModel.tblRealUser RealUser)
        {
            return (sqlHelper.RunProcedure("sp_tblRealUser_DeleteRow", RealUser) > 0);
        }
    }
}