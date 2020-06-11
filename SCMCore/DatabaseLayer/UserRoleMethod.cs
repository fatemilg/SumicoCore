using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class UserRoleMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetUserRoleData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblUserRole_GetData", search);
        }
        public DataSet GetPersonelNameInUserRole(ViewModel.tblUserRole UserRole)
        {
            return sqlHelper.returnDataSet("sp_tblUserRole_GetPersonelNameWithUnicRoleName", UserRole);
        }

        public bool AddUserRole(ViewModel.tblUserRole UserRole)
        {
            return (sqlHelper.RunProcedure("sp_tblUserRole_Insert", UserRole) > 0);
        }
        public bool DeleteUserRole(ViewModel.tblUserRole UserRole)
        {
            return (sqlHelper.RunProcedure("sp_tblUserRole_DeleteRow", UserRole,true) > 0);
        }
    }
}