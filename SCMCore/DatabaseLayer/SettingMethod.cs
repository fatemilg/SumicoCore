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
    public class SettingMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetSettingData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblSetting_GetData", search);
        }
        public bool UpdateSetting(ViewModel.tblSetting Setting)
        {
            return (sqlHelper.RunProcedure("sp_tblSetting_Update", Setting) > 0);
        }
    }
}