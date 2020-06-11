using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{
    public class StateController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIranianState()
        {
            try
            {
                Bis.StateMethod BisState = new Bis.StateMethod();
                ViewModel.tblState getState = new ViewModel.tblState();
                JArray JsonState = BisState.GetIranianState(getState);
                return Ok(JsonState);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
