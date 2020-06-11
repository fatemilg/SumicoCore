using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Data;

namespace SCMCore.DatabaseLayer
{
    public class BannerMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetBannerData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblBanner_GetData", search);
        }

        public JArray GetBannerJsonData(ViewModel.tblBanner banner)
        {
            return sqlHelper.ReturnJsonData("sp_tblBanner_GetJsonData", banner);
        }

        public bool AddBanner(ViewModel.tblBanner banner)
        {
            return (sqlHelper.RunProcedure("sp_tblBanner_Insert", banner) > 0);
        }

        public bool UpdateBanner(ViewModel.tblBanner banner)
        {
            return (sqlHelper.RunProcedure("sp_tblBanner_Update", banner) > 0);
        }
        public bool UpdateBannerPicUrl(ViewModel.tblBanner banner)
        {
            return (sqlHelper.RunProcedure("sp_tblBanner_UpdatePicUrl", banner) > 0);
        }
        public bool UpdateBannerPicUrlForMobile(ViewModel.tblBanner banner)
        {
            return (sqlHelper.RunProcedure("sp_tblBanner_UpdatePicUrlForMobile", banner) > 0);
        }
        public bool DeleteBanner(ViewModel.tblBanner banner)
        {
            return (sqlHelper.RunProcedure("sp_tblBanner_DeleteRow", banner) > 0);
        }
    }
}