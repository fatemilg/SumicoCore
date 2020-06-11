using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class PreparationPriceListDetailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetPreparationPriceListDetailWithOutCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceListDetail_GetDataWithOutCategory", PreparationPriceListDetail);
        }
        public JArray GetPreparationPriceListDetailByCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblPreparationPriceListDetail_GetDataByCategory", PreparationPriceListDetail);
        }
        public bool UpdateEndUserPriceByIDPreparationPriceListDetail(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_UpdateEndUserPriceByIDPreparationPriceListDetail", PreparationPriceListDetail) > 0);
        }
        public bool UpdateQoutationDetainInPreparationPriceListDetail(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_UpdateQoutationDetainInPreparationPriceListDetail", PreparationPriceListDetail, true) > 0);
        }

        public bool IncreaseAllEndUserPriceForWithOutCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_IncreaseAllEndUserPriceForWithOutCategory ", PreparationPriceListDetail) > 0);
        }
        public bool DecreaseAllEndUserPriceForWithOutCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_DecreaseAllEndUserPriceForWithOutCategory", PreparationPriceListDetail) > 0);
        }

        public bool IncreaseAllEndUserPriceForByCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_IncreaseAllEndUserPriceForByCategory", PreparationPriceListDetail) > 0);
        }
        public bool DecreaseAllEndUserPriceForByCategory(ViewModel.tblPreparationPriceListDetail PreparationPriceListDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblPreparationPriceListDetail_DecreaseAllEndUserPriceForByCategory", PreparationPriceListDetail) > 0);
        }

    }
}