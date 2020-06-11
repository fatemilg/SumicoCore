using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Controllers
{
    public class SiteVisitController : ApiController
    {
        Bis.SiteVisitMethod BisSiteVisit = new Bis.SiteVisitMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSiteVisitByIDPage(ViewModel.tblSiteVisit SiteVisit)
        {
            try
            {
                JArray JsonSiteVisit = BisSiteVisit.GetSiteVisitJsonData(SiteVisit);
                return Ok(JsonSiteVisit);
            }
            catch
            {
                return NotFound();
            }
        }
         [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddSiteVisit(ViewModelSite.SiteVisitor NewSiteVisit)
        {
            try
            {
                bool ret = BisSiteVisit.AddSiteVisit(NewSiteVisit);
                if (ret)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }
        }
        
    }
}