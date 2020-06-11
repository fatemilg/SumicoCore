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
    public class PriorityMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetPriorityData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblPriority_GetData", search);
        }
    }
}