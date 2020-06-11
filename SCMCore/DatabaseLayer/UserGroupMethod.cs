using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class UserGroupMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetUserGroupData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblUserGroup_GetData", search);
        }
        
        public bool AddUserGroup(ViewModel.tblUserGroup UserGroup)
        {
            return (sqlHelper.RunProcedure("sp_tblUserGroup_Insert", UserGroup) > 0);
        }
        public bool UpdateUserGroupBySort(ViewModel.tblUserGroup UserGroup)
        {
            return (sqlHelper.RunProcedure("sp_tblUserGroup_UpdateBySort", UserGroup) > 0);
        }
        public bool DeleteUserGroup(ViewModel.tblUserGroup UserGroup)
        {
            return (sqlHelper.RunProcedure("sp_tblUserGroup_DeleteRow", UserGroup) > 0);
        }
    }
}