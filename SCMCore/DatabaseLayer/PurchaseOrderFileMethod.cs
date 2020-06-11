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
    public class PurchaseOrderFileMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetPurchaseOrderFileData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrderFile_GetData", search);
        }

        public JArray GetPurchaseOrderFileAndDetail(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrderFile_GetPurchaseOrderFileAndDetail", PurchaseOrderFile);
        }
   
        public JArray CheckPartNumberInPurchaseOrderFile(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblPurchaseOrderFile_CheckPartNumberInPurchaseOrderFile", PurchaseOrderFile) ;
        }

        public bool AddPurchaseOrderFile(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPurchaseOrderFile_Insert", PurchaseOrderFile,true) > 0);
        }
        public bool UpdatePurchaseOrderFile(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPurchaseOrderFile_Update", PurchaseOrderFile) > 0);
        }
        public bool UpdateSortInPurchaseOrderFile(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPurchaseOrderFile_UpdateSort", PurchaseOrderFile) > 0);
        }
        public bool UpdateShow(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPurchaseOrderFile_UpdateShow", PurchaseOrderFile) > 0);
        }
        public bool DeletePurchaseOrderFile(ViewModel.tblPurchaseOrderFile PurchaseOrderFile)
        {
            return (sqlHelper.RunProcedure("sp_tblPurchaseOrderFile_Delete", PurchaseOrderFile,true) > 0);
        }
    }
}