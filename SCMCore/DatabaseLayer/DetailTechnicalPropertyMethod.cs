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
    public class DetailTechnicalPropertyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetDetailTechnicalPropertyData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDetailTechnicalProperty_GetData", search);
        }     
        public JArray GetJsonDetailTechnicalPropertyData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDetailTechnicalProperty_GetData", search);
        }
        public DataSet GetDetailTechnicalPropertyData_ForCompare(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDetailTechnicalProperty_GetData_ForCompare", search);
        }
        public DataSet GetValueData_ForCompare(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDetailTechnicalProperty_GetValueData_ForCompare", search);
        }
        public bool AddDetailTechnicalProperty(ViewModel.tblDetailTechnicalProperty DetailTechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailTechnicalProperty_Insert", DetailTechnicalProperty) > 0);
        }

        public bool UpdateDetailTechnicalProperty(ViewModel.tblDetailTechnicalProperty DetailTechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailTechnicalProperty_Update", DetailTechnicalProperty) > 0);
        }

        public bool DeleteDetailTechnicalProperty(ViewModel.tblDetailTechnicalProperty DetailTechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblDetailTechnicalProperty_DeleteRow", DetailTechnicalProperty) > 0);
        }
    }
}