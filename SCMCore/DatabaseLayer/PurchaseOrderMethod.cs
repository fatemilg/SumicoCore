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
    public class PurchaseOrderMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetPurchaseOrderInProgress(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrder_GetJsonDataInProgress", search);
        }
        public JArray GetPurchaseOrderByIDPurchaseOrderFile(ViewModel.tblPurchaseOrder PurchaseOrder)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrder_GetJsonDataByIDPurchaseOrderFile", PurchaseOrder);
        }
        public JArray GetPurchaseOrderByPartNumber(ViewModel.tblPurchaseOrder PurchaseOrder)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrder_GetJsonDataByPartNumber", PurchaseOrder);
        }
    }
}