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
    public class CountryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetCountryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblCountry_GetData", search);
        }

        public bool AddCountry(ViewModel.tblCountry country)
        {
            return (sqlHelper.RunProcedure("sp_tblCountry_Insert", country) > 0);
        }

        public bool UpdateCountry(ViewModel.tblCountry country)
        {
            return (sqlHelper.RunProcedure("sp_tblCountry_Update", country) > 0);
        }

        public bool DeleteCountry(ViewModel.tblCountry country)
        {
            return (sqlHelper.RunProcedure("sp_tblCountry_DeleteRow", country) > 0);
        }
    }
}