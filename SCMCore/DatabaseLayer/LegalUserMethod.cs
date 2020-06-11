using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class LegalUserMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

    
        //Customer
        public DataSet GetCutomerData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblLegalUser_GetCustomerData", search);
        }
        public bool AddCustomer(ViewModel.tblLegalUser legalUser)
        {
            return (sqlHelper.RunProcedure("sp_tblLegalUser_InsertCustomer", legalUser, true) > 0);
        }
        public bool UpdateCustomer(ViewModel.tblLegalUser legalUser)
        {
            return (sqlHelper.RunProcedure("sp_tblLegalUser_UpdateCustomer", legalUser, true) > 0);
        }
        //Supplier
        public DataSet GetSupplierData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblLegalUser_GetSupplierData", search);
        }

        public JArray GetSupplierJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblLegalUser_GetSupplierData", search);
        }


        public bool AddSupplier(ViewModel.tblLegalUser legalUser)
        {
            return (sqlHelper.RunProcedure("sp_tblLegalUser_InsertSupplier", legalUser, true) > 0);
        }
        public bool UpdateSupplier(ViewModel.tblLegalUser legalUser)
        {
            return (sqlHelper.RunProcedure("sp_tblLegalUser_UpdateSupplier", legalUser, true) > 0);
        }



        //Get Koli
        public DataSet GetCompleteLegalUserData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblLegalUser_GetCompleteData", search);
        }

        //Delete
        public bool DeleteLegalUser(ViewModel.tblLegalUser legalUser)
        {
            return (sqlHelper.RunProcedure("sp_tblLegalUser_DeleteRow", legalUser) > 0);
        }
    }
}