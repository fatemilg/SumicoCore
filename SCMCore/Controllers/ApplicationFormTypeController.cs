using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.Controllers
{
    public class ApplicationFormTypeController : ApiController
    {
        Bis.ApplicationFormTypeMethod BisApplicationFormType = new Bis.ApplicationFormTypeMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetApplicationFormType()
        {
            try
            {
                ViewModel.tblApplicationFormType ApplicationFormType = new ViewModel.tblApplicationFormType();
                JArray JsonApplicationFormType = BisApplicationFormType.GetApplicationFormTypeData(ApplicationFormType);
                return Ok(JsonApplicationFormType);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
