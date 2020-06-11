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
    public class RoleMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetRoleData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRole_GetData", search);
        }

        public bool AddRole(ViewModel.tblRole Role)
        {
            return (sqlHelper.RunProcedure("sp_tblRole_Insert", Role) > 0);
        }

        public bool UpdateRole(ViewModel.tblRole Role)
        {
            return (sqlHelper.RunProcedure("sp_tblRole_Update", Role) > 0);
        }

        public bool DeleteRole(ViewModel.tblRole Role)
        {
            return (sqlHelper.RunProcedure("sp_tblRole_DeleteRow", Role,true) > 0);
        }


    }
}