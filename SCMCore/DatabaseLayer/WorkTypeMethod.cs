using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class WorkTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetWorkTypeJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblWorkType_GetData", search);
        }
        public bool AddWorkType(ViewModel.tblWorkType tblWorkType)
        {
            return (sqlHelper.RunProcedure("sp_tblWorkType_Insert", tblWorkType) > 0);
        }
        public bool UpdateWorkType(ViewModel.tblWorkType tblWorkType)
        {
            return (sqlHelper.RunProcedure("sp_tblWorkType_Update", tblWorkType) > 0);
        }
        public bool DeleteWorkType(ViewModel.tblWorkType tblWorkType)
        {
            return (sqlHelper.RunProcedure("sp_tblWorkType_Delete", tblWorkType) > 0);
        }
    }
}

