
using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class CompanyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetCompanyJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblCompany_GetData", search);
        }
        public bool AddCompany(ViewModel.tblCompany tblCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblCompany_Insert", tblCompany) > 0);
        }
        public bool UpdateCompany(ViewModel.tblCompany tblCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblCompany_Update", tblCompany) > 0);
        }
        public bool DeleteCompany(ViewModel.tblCompany tblCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblCompany_Delete", tblCompany) > 0);
        }
    }
}






