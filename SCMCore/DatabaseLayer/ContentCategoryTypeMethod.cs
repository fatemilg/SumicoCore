using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace SCMCore.DatabaseLayer
{
    public class ContentCategoryTypeMethod
    {
        SqlHelper sqlHelper = new SqlHelper();

        public JArray GetContentCategoryTypeJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentCategoryType_GetData", search);
        }
        public JArray GetCompleteContentCategoryTypeJsonData(ViewModel.Search search)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentCategoryType_GetCompleteData", search);
        }
    }
}