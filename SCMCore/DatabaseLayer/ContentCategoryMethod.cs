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
    public class ContentCategoryMethod
    {

        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetContentCategoryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblContentCategory_GetData", search);
        }
        public JArray GetContentCategoryJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentCategory_GetData", search);
        }
        public JArray GetContentCategoryNestedJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentCategory_GetJsonData", search);
        }
        public bool AddContentCategory(ViewModel.tblContentCategory ContentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategory_Insert", ContentCategory) > 0);
        }

        public bool UpdateContentCategory(ViewModel.tblContentCategory ContentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategory_Update", ContentCategory) > 0);
        }

        public bool DeleteContentCategory(ViewModel.tblContentCategory ContentCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblContentCategory_DeleteRow", ContentCategory) > 0);
        }
    }
}