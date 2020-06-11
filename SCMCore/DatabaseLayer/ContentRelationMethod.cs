using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.DatabaseLayer
{
    public class ContentRelationMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetContentRelateDataByIDContent(ViewModel.tblContentRelate ContentRelate)
        {
            return sqlHelper.ReturnJsonData("sp_tblContentRelate_GetDataByIDContent", ContentRelate);
        }
        public bool AddContentRelate(ViewModel.tblContentRelate ContentRelate)
        {
            return (sqlHelper.RunProcedure("sp_tblContentRelate_Insert", ContentRelate) > 0);
        }
        public bool DeleteContentRelate(ViewModel.tblContentRelate ContentRelate)
        {
            return (sqlHelper.RunProcedure("sp_tblContentRelate_Delete", ContentRelate) > 0);
        }
    }
}