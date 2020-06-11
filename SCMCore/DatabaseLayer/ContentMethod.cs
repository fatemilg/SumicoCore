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
    public class ContentMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public DataSet GetContentData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblContent_GetData", search);
        }
        public DataSet GetContentDataForSitemap(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblContent_GetDataForSitemap", search);
        }
        public JArray GetContentJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContent_GetData", search);
        }
        public JArray GetFullJsonDataForSearch(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContent_GetFullJsonDataForSearch", search);
        }
        public JArray GetContentJsonDataByCategoryType(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContent_GetContentDataByCategoryType", search);
        }
        public bool AddContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_Insert", content, true) > 0);
        }
        public bool LikeContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_Like", content) > 0);
        }
        public bool UnLikeContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_UnLike", content) > 0);
        }
        public bool ToggleActivation(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_ToggleActivation", content) > 0);
        }
        public bool UpdateContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_Update", content) > 0);
        }
        public bool UpdateContentPicUrl(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_UpdatePicUrl", content) > 0);
        }
        public bool UpdatepopupForContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_UpdatePopupForContent", "") > 0);
        }
        public bool DeleteContent(ViewModel.tblContent content)
        {
            return (sqlHelper.RunProcedure("sp_tblContent_DeleteRow", content, true) > 0);
        }
    }
}