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
    public class DefineDetailProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public DataSet GetDefineDetailProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDefineDetailProduct_GetData", search);
        }
        public DataSet GetDefineDetailProductDataForSitemap(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblDefineDetailProduct_GetDataForSiteMap", search);
        }
        public DataSet GetDataSetPropertyNameValueDataByIDDefineDetailProduct(ViewModel.tblDefineDetailProduct tblDefineDetailProduct)
        {
            return sqlHelper.returnDataSet("sp_tblDefineDetailProduct_GetDataSetPropertyNameValueDataByIDDefineDetailProduct", tblDefineDetailProduct);
        }
        public JArray GetGeneratedJsonByIDProduct(ViewModel.tblDefineDetailProduct tblDefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetGeneratedJsonByIDProduct", tblDefineDetailProduct);
        }
        public JArray GetJsonPropertyNameValueDataByIDXDefine(ViewModel.tblDefineDetailProduct tblDefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetPropertyNameValueDataByIDXDefine", tblDefineDetailProduct);
        }
        public JArray GetJsonDefineDetailProductData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetData", search);
        }
        public JArray GetDefineDetailProductByGeneratedCode(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetDataByGeneratedCode", DefineDetailProduct);
        }
        public JArray GetJsonAutoCompleteDefineDetailProduct(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetAutoCompleteDefineDetailProduct", DefineDetailProduct);
        }
        public JArray GetListOfPartNumbers(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetListOfPartNumbers", search);
        }
        public JArray GetCompareListDetail(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetCompareListDetail", DefineDetailProduct);
        }
        public JArray GetTechnicalPropertiesDetailForCompare(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetTechnicalPropertiesDetailForCompare", DefineDetailProduct);
        }
        public JArray GetPropertiesDetailForCompare(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetPropertiesDetailForCompare", DefineDetailProduct);
        }
        public JArray GetDefineDetailProductJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetData", search);
        }
        public JArray GetGroupByPropertyJsonData(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.ReturnJsonData("sp_tblDefineDetailProduct_GetgroupByPropertyJsonData", DefineDetailProduct);
        }
        public DataSet GetQuickDateForParentCategory(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return sqlHelper.returnDataSet("sp_tblDefineDetailProduct_GetQuickDateForParentCategory", DefineDetailProduct);
        }
        public int AddDefineDetailProductSelectCases(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_InsertSelectCases", DefineDetailProduct, true));
        }
        public int UpdateDefineDetailProduct(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_Update", DefineDetailProduct));
        }
        public int UpdatePicUrl(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_UpdatePicUrl", DefineDetailProduct));
        }
        public bool UpdateUnderSpot(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_UpdateUnderSpot", DefineDetailProduct) > 0);
        }
        public bool UpdateSortInCrm(ViewModel.tblDefineDetailProduct DefineDetailProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_UpdateSortInCrm", DefineDetailProduct) > 0);
        }
        public bool DeleteDefineDetailProductOneRow(ViewModel.tblDefineDetailProduct RulePropertyProduct)
        {
            return (sqlHelper.RunProcedure("sp_tblDefineDetailProduct_DeleteRow_OneRow", RulePropertyProduct, true) > 0);
        }

    }
}