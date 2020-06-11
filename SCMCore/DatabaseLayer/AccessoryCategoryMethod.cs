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
    public class AccessoryCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetAccessoryCategoryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAccessoryCategory_GetData", search);
        }

        public bool AddAccessoryCategory(ViewModel.tblAccessoryCategory AccessoryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryCategory_Insert", AccessoryCategory) > 0);
        }

        public bool UpdateAccessoryCategory(ViewModel.tblAccessoryCategory AccessoryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryCategory_Update", AccessoryCategory) > 0);
        }

        public bool DeleteAccessoryCategory(ViewModel.tblAccessoryCategory AccessoryCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblAccessoryCategory_DeleteRow", AccessoryCategory) > 0);
        }
    }

}