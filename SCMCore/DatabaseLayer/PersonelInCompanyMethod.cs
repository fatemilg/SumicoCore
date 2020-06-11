using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class PersonelInCompanyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetPersonelInCompanyJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblPersonelInCompany_GetData", search);
        }
        public JArray GetPersonelInCompanyByNationalCodeAndFullName(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return sqlHelper.ReturnJsonData("sp_tblPersonelInCompany_GetPersonelInCompanyByNationalCodeAndFullName", PersonelInCompany);
        }
        public JArray GetCompletePersonelInCompanyForSite(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return sqlHelper.ReturnJsonData("sp_tblPersonelInCompany_GetCompleteDataForSite", PersonelInCompany);
        }
        public JArray GetCompleteDataByNationalCode(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return sqlHelper.ReturnJsonData("sp_tblPersonelInCompany_GetByNationalCode", PersonelInCompany);
        }
        public bool AddPersonelInCompany(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonelInCompany_Insert", PersonelInCompany, true) > 0);
        }

        public bool UpdatePersonelInCompany(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonelInCompany_Update", PersonelInCompany, true) > 0);
        }
        public bool DeletePersonelInCompany(ViewModel.tblPersonelInCompany PersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblPersonelInCompany_Delete", PersonelInCompany, true) > 0);
        }
    }
}






