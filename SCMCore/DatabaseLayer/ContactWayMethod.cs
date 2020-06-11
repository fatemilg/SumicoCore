using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Data;
using ViewModel = SCMCore.ViewModel;
namespace SCMCore.DatabaseLayer
{
    public class ContactWayMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContactWayJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContactWay_GetData", search);
        }
        public bool AddContactWay(ViewModel.tblContactWay tblContactWay)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWay_Insert", tblContactWay) > 0);
        }
        public bool Unsubscribe_To_True(ViewModel.tblContactWay tblContactWay)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWay_Update_Unsubscribe_To_True", tblContactWay) > 0);
        }
        public bool UpdateContactWay(ViewModel.tblContactWay tblContactWay)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWay_Update", tblContactWay) > 0);
        }
        public bool UpdateMainContactWayAndUser(ViewModel.tblContactWay tblContactWay)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWay_UpdateMainContactWayAndUser", tblContactWay,true) > 0);
        }
        public bool DeleteContactWay(ViewModel.tblContactWay tblContactWay)
        {
            return (sqlHelper.RunProcedure("sp_tblContactWay_Delete", tblContactWay) > 0);
        }
    }
}






