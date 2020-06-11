using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.DatabaseLayer
{
    public class AttachSiteMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public DataSet GetAttachSiteData(ViewModel.Search search)
        {
            return sqlHelper.returnDataSet("sp_tblAttachSite_GetData", search);
        }

        public bool AddAttachSite(ViewModel.tblAttachSite AttachSite)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachSite_Insert", AttachSite, true) > 0);
        }
        public bool UpdateAttachSite(ViewModel.tblAttachSite AttachSite)
        {
            return (sqlHelper.RunProcedure("sp_tblAttachSite_Update", AttachSite, true) > 0);
        }
    }
}