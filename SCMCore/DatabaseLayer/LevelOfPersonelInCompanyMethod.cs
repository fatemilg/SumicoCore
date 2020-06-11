using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class LevelOfPersonelInCompanyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetLevelOfPersonelInCompanyJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblLevelOfPersonelInCompany_GetData", search);
        }
        public bool AddLevelOfPersonelInCompany(ViewModel.tblLevelOfPersonelInCompany tblLevelOfPersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblLevelOfPersonelInCompany_Insert", tblLevelOfPersonelInCompany) > 0);
        }
        public bool UpdateLevelOfPersonelInCompany(ViewModel.tblLevelOfPersonelInCompany tblLevelOfPersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblLevelOfPersonelInCompany_Update", tblLevelOfPersonelInCompany) > 0);
        }
        public bool DeleteLevelOfPersonelInCompany(ViewModel.tblLevelOfPersonelInCompany tblLevelOfPersonelInCompany)
        {
            return (sqlHelper.RunProcedure("sp_tblLevelOfPersonelInCompany_Delete", tblLevelOfPersonelInCompany) > 0);
        }
    }
}






