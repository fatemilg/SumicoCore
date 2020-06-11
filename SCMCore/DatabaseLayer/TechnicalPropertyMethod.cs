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
    public class TechnicalPropertyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetTechnicalPropertyData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblTechnicalProperty_GetData", search);
        }

        public bool AddTechnicalProperty(ViewModel.tblTechnicalProperty TechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblTechnicalProperty_Insert", TechnicalProperty) > 0);
        }

        public bool UpdateTechnicalProperty(ViewModel.tblTechnicalProperty TechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblTechnicalProperty_Update", TechnicalProperty) > 0);
        }

        public bool DeleteTechnicalProperty(ViewModel.tblTechnicalProperty TechnicalProperty)
        {
            return (sqlHelper.RunProcedure("sp_tblTechnicalProperty_DeleteRow", TechnicalProperty) > 0);
        }
    }
}