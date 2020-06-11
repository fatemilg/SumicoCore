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
    public class LogUserMethods
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetLogUserData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_LogUser_GetData", search);
        }
        public JArray GetActiveUsersInLast24Hours(ViewModel.tblLogUser search)
        {
            return sqlHelper.ReturnJsonData("sp_GetActiveUsersInLast24Hours", search);
        }
        public bool AddLogUser(ViewModel.tblLogUser category)
        {
            return (sqlHelper.RunProcedure("sp_LogUser_Insert", category) > 0);
        }
    }
}