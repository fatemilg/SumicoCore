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
    public class StateMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetStateData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblState_GetData", search);
        }
        public JArray GetIranianState(ViewModel.tblState State)
        {
            return sqlHelper.ReturnJsonData("sp_tblState_GetIranianState", State);
        }
    }
}