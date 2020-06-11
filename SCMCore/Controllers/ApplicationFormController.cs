using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.Controllers
{
    public class ApplicationFormController : ApiController
    {
        Bis.ApplicationFormMethod BisApplicationForm = new Bis.ApplicationFormMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetApplicationForm()
        {
            try
            {
                ViewModel.tblApplicationForm ApplicationForm = new ViewModel.tblApplicationForm();
                JArray JsonUserSite = BisApplicationForm.GetApplicationFormData(ApplicationForm);
                return Ok(JsonUserSite);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddApplicationForm(VMSite.ApplicationForm obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool ret = BisApplicationForm.AddApplicationForm(obj);
                    if (ret)
                    {
                        return Ok(ret);

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
            else
            {
                PublicFunctions getError = new PublicFunctions();
                return Content(HttpStatusCode.NotFound, getError.GetErrorListFromModelState(ModelState));
            }
        }
    }
}
