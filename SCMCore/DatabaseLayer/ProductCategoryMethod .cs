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
    public class ProductCategoryMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetProductCategoryJsonDataFromParentToMaster(ViewModel.tblProductCategory ProductCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetDataFromParentToMaster", ProductCategory);
        }
        public JArray GetJsonDataFromParentToChildForSite(ViewModel.tblProductCategory ProductCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetJsonDataFromParentToChildForSite", ProductCategory);
        }
        public JArray GetDataWithMasterProductForSOL(ViewModel.tblProductCategory ProductCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetDataWithMasterProductForSOL", ProductCategory);
        }
        public JArray GetProductCategoryFromChildToParentForSiteMapMenu(ViewModel.tblProductCategory ProductCategory)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetDataFromChildToParentForSiteMapMenu", ProductCategory);
        }
        public JArray GetProductCategoryJsonDataSimple(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetDataSimple", search);
        }
        public DataSet GetProductCategoryData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductCategory_GetData", search);
        }
        public DataSet GetProductCategoryDataNormal(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductCategory_GetDataNormal", search);
        }
        public DataSet GetProductCategoryDataForSitemap(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductCategory_GetDataForSitemap", search);
        }
        public JArray GetProductCategoryJsonDataNormal(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblProductCategory_GetJsonDataNormal", search);
        }
        public DataSet GetProductCategoryDataShowInTree(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProductCategory_GetDataForShowTree", search);
        }
        public DataSet GetDataFromChildToParent(ViewModel.tblProductCategory ProductCategory)
        {
            return sqlHelper.returnDataSet("sp_tblProductCategory_GetDataFromChildToParent", ProductCategory);
        }
        public bool AddProductCategory(ViewModel.tblProductCategory ProductCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblProductCategory_Insert", ProductCategory) > 0);
        }

        public bool UpdateProductCategory(ViewModel.tblProductCategory ProductCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblProductCategory_Update", ProductCategory) > 0);
        }
        public bool UpdateProductCategoryPicUrl(ViewModel.tblProductCategory ProductCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblProductCategory_UpdatePicUrl", ProductCategory) > 0);
        }
        public bool DeleteProductCategory(ViewModel.tblProductCategory ProductCategory)
        {
            return (sqlHelper.RunProcedure("sp_tblProductCategory_DeleteRow", ProductCategory) > 0);
        }
    }
}