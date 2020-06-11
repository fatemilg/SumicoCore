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
    public class RoleMenuMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetRoleMenuData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRoleMenu_GetData", search);
        }

        public bool UpdateRoleMenu(ViewModel.tblRoleMenu RoleMenu)
        {
            return (sqlHelper.RunProcedure("sp_tblRoleMenu_Update", RoleMenu,true) > 0);
        }
        public bool InserDataFromMenuToRoleMenu(ViewModel.tblRoleMenu RoleMenu)
        {
            return (sqlHelper.RunProcedure("sp_tblRoleMenu_InserDataFromMenuToRoleMenu", RoleMenu) > 0);
        }

    }
}