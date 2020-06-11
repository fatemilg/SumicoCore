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
    public class SentEmailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetSentEmailData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblSentEmail_GetData", search);
        }
        public JArray GetSentEmailJsonData_ByIDRet(ViewModel.tblSentEmail SentEmail)
        {
            return sqlHelper.ReturnJsonData("sp_tblSentEmail_GetData_ByIDRet", SentEmail);
        }
        public bool AddSentEmail(ViewModel.tblSentEmail SentEmail)
        {
            return (sqlHelper.RunProcedure("sp_tblSentEmail_Insert", SentEmail) > 0);
        }
        public bool PreparationForNewsLetter(ViewModel.tblSentEmail SentEmail)
        {
            return (sqlHelper.RunProcedure("sp_tblSentEmail_PreparationForNewsLetter", SentEmail) > 0);
        }
        public bool UpdateSentEmail(ViewModel.tblSentEmail SentEmail)
        {
            return (sqlHelper.RunProcedure("sp_tblSentEmail_Update", SentEmail) > 0);
        }

        public bool DeleteSentEmail(ViewModel.tblSentEmail SentEmail)
        {
            return (sqlHelper.RunProcedure("sp_tblSentEmail_DeleteRow", SentEmail) > 0);
        }
    }
}