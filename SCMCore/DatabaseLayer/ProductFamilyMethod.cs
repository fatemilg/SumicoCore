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
    public class ProductFamilyMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetProductFamilyData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductFamily_GetData", search);
        }

        public bool AddProductFamily(ViewModel.tblProductFamily ProductFamily)
        {
            return (sqlHelper.RunProcedure("sp_tblProductFamily_Insert", ProductFamily) > 0);
        }

        public bool UpdateProductFamily(ViewModel.tblProductFamily ProductFamily)
        {
            return (sqlHelper.RunProcedure("sp_tblProductFamily_Update", ProductFamily) > 0);
        }

        public bool DeleteProductFamily(ViewModel.tblProductFamily ProductFamily)
        {
            return (sqlHelper.RunProcedure("sp_tblProductFamily_DeleteRow", ProductFamily) > 0);
        }
    }

}