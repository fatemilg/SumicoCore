using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class PreparationPriceListMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetPreparationPriceList(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceList_GetData", PreparationPriceList);
        }
        public JArray GetPreparationPriceListByIDPeyvastPriceFile(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceList_GetDataBYIDPeyvastPriceFile", PreparationPriceList);
        }
        public JArray GetPreparationPriceListByIDSupplierPriceListFile(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceList_GetDataByIDSupplierPriceListFile", PreparationPriceList);
        }
        public JArray GetPreparationPriceListByIDQouationFile(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceList_GetDataByIDQouationFile", PreparationPriceList);
        }

        public bool AddPreparationPriceList(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceList_Insert", PreparationPriceList, true) > 0);
        }
        public bool UpdatePreparationPriceList(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceList_Update", PreparationPriceList) > 0);
        }
        public bool UpdatePreparationPriceListByShow(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceList_UpdateShow", PreparationPriceList) > 0);
        }
        public bool DeletePreparationPriceList(ViewModel.tblPreparationPriceList PreparationPriceList)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceList_Delete", PreparationPriceList, true) > 0);
        }
    }
}