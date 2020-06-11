using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class ContentModuleController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentModuleMethod BisContentModule = new Bis.ContentModuleMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentModule()
        {
            try
            {
                ViewModel.tblContentModule ContentModule = new ViewModel.tblContentModule();
                JArray JsonContentModule = BisContentModule.GetContentModuleJsonData(ContentModule);
                return Ok(JsonContentModule);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        
    }
}



