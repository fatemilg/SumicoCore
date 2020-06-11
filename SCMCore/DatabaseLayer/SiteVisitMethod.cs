using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.DatabaseLayer
{
    public class SiteVisitMethod
    {
        SqlHelper sqlHelper = new SqlHelper();
        public JArray GetSiteVisitJsonData(ViewModel.tblSiteVisit tblSiteVisit)
        {
            return sqlHelper.ReturnJsonData("sp_tblSiteVisit_GetData", tblSiteVisit);
        }
        public bool AddSiteVisit(ViewModelSite.SiteVisitor tblSiteVisit)
        {
            return (sqlHelper.RunProcedure("sp_tblSiteVisit_Insert", tblSiteVisit, true) > 0);
        }
    }
}