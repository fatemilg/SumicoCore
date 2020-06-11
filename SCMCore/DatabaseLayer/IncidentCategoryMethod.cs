using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
namespace SCMCore.DatabaseLayer
{
    public class IncidentCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetIncidentCategoryJsonData(ViewModel.tblIncidentCategory tblIncidentCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncidentCategory_GetData", tblIncidentCategory);
        }
        public JArray GetDataWithIncidents(ViewModel.tblIncidentCategory tblIncidentCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblIncidentCategory_GetDataWithIncidents", tblIncidentCategory);
        }
        public bool AddIncidentCategory(ViewModel.tblIncidentCategory tblIncidentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblIncidentCategory_Insert", tblIncidentCategory) > 0);
        }
        public bool UpdateIncidentCategory(ViewModel.tblIncidentCategory tblIncidentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblIncidentCategory_Update", tblIncidentCategory) > 0);
        }
        public bool DeleteIncidentCategory(ViewModel.tblIncidentCategory tblIncidentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblIncidentCategory_Delete", tblIncidentCategory) > 0);
        }
    }
}






