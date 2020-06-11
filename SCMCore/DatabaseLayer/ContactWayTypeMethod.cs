using Newtonsoft.Json.Linq;
using SCMCore.Classes;
namespace SCMCore.DatabaseLayer
{
    public class ContactWayTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContactWayTypeJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContactWayType_GetData", search);
        }
        public bool AddContactWayType(ViewModel.tblContactWayType tblContactWayType)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWayType_Insert", tblContactWayType) > 0);
        }
        public bool UpdateContactWayType(ViewModel.tblContactWayType tblContactWayType)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWayType_Update", tblContactWayType) > 0);
        }
        public bool DeleteContactWayType(ViewModel.tblContactWayType tblContactWayType)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWayType_Delete", tblContactWayType) > 0);
        }
    }
}






