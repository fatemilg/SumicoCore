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
    public class RelatedDefineDetailProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetRelatedDefineDetailProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRelatedDefineDetailProduct_GetData", search);
        }
        public DataSet GetAllRelations(ViewModel.tblRelatedDefineDetailProduct RelatedDefineDetailProduct)
        {
            return sqlHelper.returnDataSet("sp_tblRelatedDefineDetailProduct_GetRelatedDefineDetailProductData", RelatedDefineDetailProduct);
        }
        public JArray GetJsonAllRelations(ViewModel.tblRelatedDefineDetailProduct RelatedDefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("[sp_tblRelatedDefineDetailProduct_GetRelatedDefineDetailProductJsonData]", RelatedDefineDetailProduct);
        }
        public bool AddRelatedDefineDetailProduct(ViewModel.tblRelatedDefineDetailProduct RelatedDefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblRelatedDefineDetailProduct_Insert", RelatedDefineDetailProduct) > 0);
        }

        public bool DeleteRow(ViewModel.tblRelatedDefineDetailProduct RelatedDefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblRelatedDefineDetailProduct_DeleteRow", RelatedDefineDetailProduct) > 0);
        }
    }
}