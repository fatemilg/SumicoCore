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
    public class UnitMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetUnitData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblUnit_GetData", search);
        }

        public bool AddUnit(ViewModel.tblUnit Unit)
        {
            return (sqlHelper.RunProcedure("sp_tblUnit_Insert", Unit) > 0);
        }

        public bool UpdateUnit(ViewModel.tblUnit Unit)
        {
            return (sqlHelper.RunProcedure("sp_tblUnit_Update", Unit) > 0);
        }

        public bool DeleteUnit(ViewModel.tblUnit Unit)
        {
            return (sqlHelper.RunProcedure("sp_tblUnit_DeleteRow", Unit) > 0);
        }
    }
}