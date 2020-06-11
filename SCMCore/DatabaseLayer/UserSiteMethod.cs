using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.DatabaseLayer
{
    public class UserSiteMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetUserSiteJson(ViewModel.tblUserSite user)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_GetData", user);
        }
        public JArray GetIntegratedUserSiteByID(ViewModel.tblUserSite user)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_GetIntegratedUserSiteByID", user);
        }
        public JArray CheckPersonelInCompanyExistInUserSite(ViewModel.tblUserSite user)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_CheckPersonelInCompanyExistInUserSite", user);
        }
        public JArray LoginUserSite(VMSite.SignIn SignIn)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_Login", SignIn);
        }
        public JArray LoginUserSiteByToken(ViewModel.tblLogUser Login)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_LoginByToken", Login);
        }
        public JArray GetCountNewUserNotShown(ViewModel.tblUserSite user)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_GetCountNewUserNotShown", user);
        }
        public JArray GetUserSiteJsonDataByNationalCode(ViewModel.tblUserSite UserSite)
        {
            return sqlHelper.ReturnJsonData("sp_tblUserSite_GetByNationalCode", UserSite);
        }
        public bool AddUserSiteByEnroll(VMSite.SignUp SignUp)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_InsertByEnroll", SignUp, true) > 0);
        }
        public bool AddForDynamicForm(VMSite.UserSiteDynamicForm DynamicForm)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_InsertForDynamicForm", DynamicForm) > 0);
        }
        public bool AddUserSiteIntoPersonelInCompany(ViewModel.tblUserSite UserSite)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_AddUserSiteIntoPersonelInCompany", UserSite, true) > 0);
        }
        public bool UpdateShowDate(ViewModel.tblUserSite UserSite)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_UpdateShowDate", UserSite) > 0);
        }
        public bool UpdateIDPersonelInCompany(ViewModel.tblUserSite UserSite)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_UpdateIDPersonelInCompany", UserSite,true) > 0);
        }
        public bool DeleteUserSite(ViewModel.tblUserSite UserSite)
        {
            return (sqlHelper.RunProcedure("sp_tblUserSite_DeleteRow", UserSite) > 0);
        }
    }
}