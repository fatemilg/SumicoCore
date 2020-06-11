using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class ContactWayController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContactWayMethod BisContactWay = new Bis.ContactWayMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContactWay()
        {
            try
            {
                ViewModel.Search ContactWaySearch = new ViewModel.Search();
                ContactWaySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContactWay = BisContactWay.GetContactWayJsonData(ContactWaySearch);
                return Ok(JsonContactWay);
            }
            catch
            {
                return NotFound();
            }
        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContactWay_ByIDUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search ContactWaySearch = new ViewModel.Search();
                ContactWaySearch.Filter = " And tblContactWay.IDUser = '" + JsonObject["IDUser"].ToString() + "'";
                ContactWaySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContactWay = BisContactWay.GetContactWayJsonData(ContactWaySearch);
                return Ok(JsonContactWay);
            }
            catch
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContactWay(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWay NewContactWay = JsonObject.ToObject<ViewModel.tblContactWay>();
                bool ret = BisContactWay.AddContactWay(NewContactWay);
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
        public IHttpActionResult UpdateContactWay(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWay UpdateContactWay = JsonObject.ToObject<ViewModel.tblContactWay>();
                bool ret = BisContactWay.UpdateContactWay(UpdateContactWay);
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
        public IHttpActionResult UpdateMainContactWayAndUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWay UpdateContactWay = JsonObject.ToObject<ViewModel.tblContactWay>();
                bool ret = BisContactWay.UpdateMainContactWayAndUser(UpdateContactWay);
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
        public IHttpActionResult DeleteContactWay(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContactWay DelContactWay = JsonObject.ToObject<ViewModel.tblContactWay>();
                bool ret = BisContactWay.DeleteContactWay(DelContactWay);
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



