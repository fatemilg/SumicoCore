using System;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;


namespace SCMCore.DatabaseLayer
{
    public class MadeCountryMethods
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetMadeCountryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblMadeCountry_GetData", search);
        }
    }
}