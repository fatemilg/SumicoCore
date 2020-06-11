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
    public class ProductMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetProductData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetData", search);
        }
        public DataSet GetProductDataForSitemap(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataForSitemap", search);
        }
        public DataSet GetProductDataQuick(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataQuick", search);
        }
        public DataSet GetProductDataQuick_Small(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataQuick_Small", search);
        }
        public JArray GetProductJsonDataQuick_Small(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblProduct_GetJsonDataQuick_Small", search);
        }
        public DataSet GetDataQuickWithPrice(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataQuickWithPrice", search);
        }
        public DataSet GetDataWithProductCategory_Property(ViewModel.tblProduct Product)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataWithProductCategory_Property", Product);
        }

        public DataSet GetDataAutoCompeleForCompare(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetDataAutoCompeleForCompare", search);
        }

        public bool AddProduct(ViewModel.tblProduct Product)
        {
            return (sqlHelper.RunProcedure("sp_tblProduct_Insert", Product) > 0);
        }

        public bool UpdateProduct(ViewModel.tblProduct Product)
        {
            return (sqlHelper.RunProcedure("sp_tblProduct_Update", Product) > 0);
        }
        public bool UpdateProductProductUrl(ViewModel.tblProduct Product)
        {
            return (sqlHelper.RunProcedure("sp_tblProduct_UpdateProductUrl", Product) > 0);
        }

        public bool DeleteProduct(ViewModel.tblProduct Product)
        {
            return (sqlHelper.RunProcedure("sp_tblProduct_DeleteRow", Product) > 0);
        }
        public DataSet GetQuickDateForParentCategory(ViewModel.tblProduct Product)
        {
            return sqlHelper.returnDataSet("sp_tblProduct_GetQuickDateForParentCategory", Product);
        }
    }
}