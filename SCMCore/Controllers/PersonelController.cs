using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class PersonelController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();


       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPersonel(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();
                ViewModel.Search get = new ViewModel.Search();
                get.Filter = " And tblPersonel.IDUser = '" + AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()) + "' ";
                get.JsonResult = " FOR JSON PATH";
                JArray JsonPersonel = BisPersonel.GetPersoneJsonlData(get);
                return Ok(JsonPersonel);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
