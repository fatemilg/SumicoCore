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
    public class MenuMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetMenuData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblMenu_GetData", search);
        }
        public JArray GetMenuJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblMenu_GetJsonData", search);
        }
        public JArray GetPersonelOfMenuData(ViewModel.tblMenu menu)
        {
            return sqlHelper.ReturnJsonData("sp_tblMenu_GetPersonelOfMenuData", menu);
        }
        public DataSet GetCompleteChildMenu(ViewModel.tblMenu menu)
        {
            return sqlHelper.returnDataSet("sp_tblMenu_GetCompleteChildMenu", menu);
        }

        public bool AddMenu(ViewModel.tblMenu menu)
        {
            return (sqlHelper.RunProcedure("sp_tblMenu_Insert", menu) > 0);
        }

        public bool UpdateMenu(ViewModel.tblMenu menu)
        {
            return (sqlHelper.RunProcedure("sp_tblMenu_Update", menu) > 0);
        }

        public bool DeleteMenu(ViewModel.tblMenu menu)
        {
            return (sqlHelper.RunProcedure("sp_tblMenu_DeleteRow", menu) > 0);
        }
    }
}