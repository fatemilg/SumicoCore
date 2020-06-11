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
    public class DictionaryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetDictionaryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDictionary_GetData", search);
        }
        public JArray GetDictionaryJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDictionary_GetData", search);
        }
        public bool AddDictionary(ViewModel.tblDictionary Dictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblDictionary_Insert", Dictionary) > 0);
        }
        public bool UpdateDictionary(ViewModel.tblDictionary Dictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblDictionary_Update", Dictionary) > 0);
        }
        public bool DeleteDictionary(ViewModel.tblDictionary Dictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblDictionary_DeleteRow", Dictionary) > 0);
        }
        public int UpdatePicUrl(ViewModel.tblDictionary Dictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblDictionary_UpdatePicUrl", Dictionary));
        }
        public bool ToggleActivation(ViewModel.tblDictionary Dictionary)
        {
            return (sqlHelper.RunProcedure("sp_tblDictionary_ToggleActivation", Dictionary) > 0);
        }
        public JArray GetOneRecordRandom(ViewModel.tblDictionary Dictionary)
        {
            return sqlHelper.ReturnJsonData("sp_tblDictionary_GetDataOneRecordRandom", Dictionary);
        }
    }
}