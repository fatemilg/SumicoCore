using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class OrganizationalPositionController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.OrganizationalPositionMethod BisOrganizationalPosition = new Bis.OrganizationalPositionMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetOrganizationalPosition()
        {
            try
            {
                ViewModel.Search OrganizationalPositionSearch = new ViewModel.Search();
                OrganizationalPositionSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonOrganizationalPosition = BisOrganizationalPosition.GetOrganizationalPositionJsonData(OrganizationalPositionSearch);
                return Ok(JsonOrganizationalPosition);
            }
            catch
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddOrganizationalPosition(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblOrganizationalPosition NewOrganizationalPosition = JsonObject.ToObject<ViewModel.tblOrganizationalPosition>();
                bool ret = BisOrganizationalPosition.AddOrganizationalPosition(NewOrganizationalPosition);
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
        public IHttpActionResult UpdateOrganizationalPosition(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblOrganizationalPosition UpdateOrganizationalPosition = JsonObject.ToObject<ViewModel.tblOrganizationalPosition>();
                bool ret = BisOrganizationalPosition.UpdateOrganizationalPosition(UpdateOrganizationalPosition);
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
        public IHttpActionResult DeleteOrganizationalPosition(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblOrganizationalPosition DelOrganizationalPosition = JsonObject.ToObject<ViewModel.tblOrganizationalPosition>();
                bool ret = BisOrganizationalPosition.DeleteOrganizationalPosition(DelOrganizationalPosition);
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
    }
}



