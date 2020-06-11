using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
namespace SCMCore.DatabaseLayer
{
    public class IncidentMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetIncidentJsonData(ViewModel.tblIncident tblIncident)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncident_GetData", tblIncident);
        }
        public JArray GetIncidentJsonData_ByIDIncident(ViewModel.tblIncident tblIncident)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncident_GetData_ByIDIncident", tblIncident);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncident_GetFullJsonDataForSearch", search);
        }
        public JArray GetIncidentJsonData_ByIDX(ViewModel.tblIncident tblIncident)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncident_GetData_ByIDX", tblIncident);
        }
        public bool AddIncident(ViewModel.tblIncident tblIncident)
        {
            return (sqlHelper.RunProcedure("sp_tblIncident_Insert", tblIncident) > 0);
        }
        public bool UpdateIncident(ViewModel.tblIncident tblIncident)
        {
            return (sqlHelper.RunProcedure("sp_tblIncident_Update", tblIncident) > 0);
        }
        public bool DeleteIncident(ViewModel.tblIncident tblIncident)
        {
            return (sqlHelper.RunProcedure("sp_tblIncident_Delete", tblIncident) > 0);
        }
    }
}






