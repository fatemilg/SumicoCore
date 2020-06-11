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
    public class AccessLevelMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetAccessLevelDataForTree(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAccessLevel_GetDataForTree", search);
        }
        public JArray GetJsonAccessLevelDataForTree(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblAccessLevel_GetDataForTree", search);
        }
        public JArray GetJsonDataForEventUser(ViewModel.tblAccessLevel AccessLevel)
        {
            return sqlHelper.ReturnJsonData("sp_tblAccessLevel_GetDataForEventUser", AccessLevel);
        }
        public bool UpdateAccessLevel(ViewModel.tblAccessLevel AccessLevel)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessLevel_Update", AccessLevel) > 0);
        }

        public bool InserDataFromMenuToAccessLevelAndGetData(ViewModel.tblAccessLevel AccessLevel)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessLevel_InserDataFromMenuToAccessLevel", AccessLevel) > 0);
        }

    }
}