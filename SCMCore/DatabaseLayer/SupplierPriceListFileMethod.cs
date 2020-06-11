using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class SupplierPriceListFileMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetSupplierPriceListFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return sqlHelper.ReturnJsonData("sp_tblSupplierPriceListFile_GetSupplierPriceListFile", SupplierPriceListFile);
        }

        public bool AddSupplierPriceListFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return (sqlHelper.RunProcedure("sp_tblSupplierPriceListFile_Insert", SupplierPriceListFile, true) > 0);
        }
        public bool UpdateRatioSupplierPriceListFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return (sqlHelper.RunProcedure("sp_tblSupplierPriceListFile_UpdateRatio", SupplierPriceListFile,true) > 0);
        }
        public bool UpdateSupplierPriceListFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return (sqlHelper.RunProcedure("sp_tblSupplierPriceListFile_Update", SupplierPriceListFile) > 0);
        }
        public bool ChangeSortInSupplierPriceFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return (sqlHelper.RunProcedure("sp_tblSupplierPriceListFile_UpdateSort", SupplierPriceListFile) > 0);
        }
        public bool DeleteSupplierPriceListFile(ViewModel.tblSupplierPriceListFile SupplierPriceListFile)
        {
            return (sqlHelper.RunProcedure("sp_tblSupplierPriceListFile_Delete", SupplierPriceListFile, true) > 0);
        }
 
    }
}