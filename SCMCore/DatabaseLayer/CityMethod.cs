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
    public class CityMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetCityData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblCity_GetData", search);
        }

        public bool AddCity(ViewModel.tblCity City)
        {
            return (sqlHelper.RunProcedure("sp_tblCity_Insert", City) > 0);
        }

        public bool UpdateCity(ViewModel.tblCity City)
        {
            return (sqlHelper.RunProcedure("sp_tblCity_Update", City) > 0);
        }

        public bool DeleteCity(ViewModel.tblCity City)
        {
            return (sqlHelper.RunProcedure("sp_tblCity_DeleteRow", City) > 0);
        }
    }
}