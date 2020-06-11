using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.DatabaseLayer
{
    public class EventUserMethods
    {
        SqlHelper sqlHelper = new SqlHelper();
        public DataSet GetEventUserData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblEventUser_GetData", search);
        }
        
        public DataSet CheckForAccessInEventUser(ViewModel.tblEventUser EventUser)
        {
            return sqlHelper.returnDataSet("sp_tblEventUser_CheckForAccessInEventUser", EventUser);
        }
        public JArray GetEventUser_ByIDUser(ViewModel.tblEventUser EventUser)
        {
            return sqlHelper.ReturnJsonData("sp_tblEventUser_GetEventUser_ByIDUser", EventUser);
        }
        public bool AddEventUser(ViewModel.tblEventUser EventUser)
        {
            return (sqlHelper.RunProcedure("sp_tblEventUser_Insert", EventUser) > 0);
        }

        public bool DeleteEventUser(ViewModel.tblEventUser EventUser)
        {
            return (sqlHelper.RunProcedure("sp_tblEventUser_DeleteRow", EventUser) > 0);
        }
    }
}