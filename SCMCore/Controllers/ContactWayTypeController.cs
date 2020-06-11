using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class ContactWayTypeController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContactWayTypeMethod BisContactWayType = new Bis.ContactWayTypeMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContactWayType()
        {
            try
            {
                ViewModel.Search ContactWayTypeSearch = new ViewModel.Search();
                ContactWayTypeSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContactWayType = BisContactWayType.GetContactWayTypeJsonData(ContactWayTypeSearch);
                return Ok(JsonContactWayType);
            }
            catch
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContactWayType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWayType NewContactWayType = JsonObject.ToObject<ViewModel.tblContactWayType>();
                bool ret = BisContactWayType.AddContactWayType(NewContactWayType);
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
        public IHttpActionResult UpdateContactWayType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWayType UpdateContactWayType = JsonObject.ToObject<ViewModel.tblContactWayType>();
                bool ret = BisContactWayType.UpdateContactWayType(UpdateContactWayType);
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
        public IHttpActionResult DeleteContactWayType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWayType DelContactWayType = JsonObject.ToObject<ViewModel.tblContactWayType>();
                bool ret = BisContactWayType.DeleteContactWayType(DelContactWayType);
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



