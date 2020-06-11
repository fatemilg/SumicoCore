using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class BannerController : ApiController
    {
        Bis.BannerMethod BisBanner = new Bis.BannerMethod();


        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetBanner(ViewModel.tblBanner obj)
        {
            try
            {
                JArray ret = BisBanner.GetBannerJsonData(obj);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
