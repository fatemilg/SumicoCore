using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class QouationDetailMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetQouationDetail(ViewModel.tblQouationDetail QouationDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblQouationDetail_GetQouationDetail", QouationDetail);
        }
        public JArray GetSumsColumnsOfQouationDetail(ViewModel.tblQouationDetail QouationDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblQouationDetail_GetSumsColumns", QouationDetail);
        }
        public JArray GetQouationDetailByPartNumber(ViewModel.tblQouationDetail QouationDetail)
        {
            return sqlHelper.ReturnJsonData("sp_tblQouationDetail_GetJsonDataByPartNumber", QouationDetail);
        }
        public bool AddAllQouationDetail(ViewModel.tblQouationDetail QouationDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationDetail_AddAllQouationDetail", QouationDetail,true) > 0);
        }
        public bool DeleteSelectedQouationDetail(ViewModel.tblQouationDetail QouationDetail)
        {
            return (sqlHelper.RunProcedure("sp_tblQouationDetail_DeleteSelectedQouationDetail", QouationDetail) > 0);
        }
    }
}