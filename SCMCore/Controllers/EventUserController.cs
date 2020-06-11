using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class EventUserController : ApiController
    {
        Bis.EventUserMethods BisEventUser = new Bis.EventUserMethods();
        AuthorizationUser AuUser = new AuthorizationUser();
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddEventUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblEventUser NewEventUser = JsonObject.ToObject<ViewModel.tblEventUser>();
                bool ret = BisEventUser.AddEventUser(NewEventUser);
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
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetEventUser_ByIDUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblEventUser GetEventUser = new ViewModel.tblEventUser();
                GetEventUser.IDUser = AuUser.ReturnIDUser(JsonObject["IDLogUser"].ObjectToGuid());
                string MenuUrl = JsonObject["MenuUrl"].ToString();
                GetEventUser.MenuUrl = MenuUrl.Replace(MenuUrl.Split('/')[0] + "/" + MenuUrl.Split('/')[1] + "/" + MenuUrl.Split('/')[2] + "/", "");

                JArray JsonHaveAccess = BisEventUser.GetEventUser_ByIDUser(GetEventUser);
                return Ok(JsonHaveAccess);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}