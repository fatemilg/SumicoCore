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
    public class RulePropertyProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetRulePropertyProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetData", search);
        }

        public DataSet GetRulePropertyProductFullData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetFullData", search);
        }
        public DataSet GetFullDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetFullDataForSearch", search);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblRulePropertyProduct_GetFullJsonDataForSearch", search);
        }
        public JArray GetFullJsonDataForSearchForSumico(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblRulePropertyProduct_GetFullJsonDataForSearchForSumico", search);
        }
        public DataSet GetRulePropertyProductData_DistinctProduct(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetData_DistinctProduct", search);
        }
        public DataSet GetCommonDefineDetailProduct(ViewModel.tblRulePropertyProduct RulePropertyProduct)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetCommonDefineDetailProduct", RulePropertyProduct);
        }
        public int UpdateRulesFromListID(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.RunProcedure("sp_tblRulePropertyProduct_UpdateRulesFromListID", DefineDetailProduct, true);
        }
        public DataSet GetDataForOrderInSite(ViewModel.tblRulePropertyProduct RulePropertyProduct)
        {
            return sqlHelper.returnDataSet("sp_tblRulePropertyProduct_GetDataForOrderInSite", RulePropertyProduct);
        }


    }
}