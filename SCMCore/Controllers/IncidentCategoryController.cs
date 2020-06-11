
using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class IncidentCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.IncidentCategoryMethod BisIncidentCategory = new Bis.IncidentCategoryMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIncidentCategory()
        {
            try
            {
                ViewModel.tblIncidentCategory tblIncidentCategory = new ViewModel.tblIncidentCategory();
                JArray JsonIncidentCategory = BisIncidentCategory.GetIncidentCategoryJsonData(tblIncidentCategory);
                return Ok(JsonIncidentCategory);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDataWithIncidents()
        {
            try
            {
                ViewModel.tblIncidentCategory tblIncidentCategory = new ViewModel.tblIncidentCategory();
                JArray JsonIncidentCategory = BisIncidentCategory.GetDataWithIncidents(tblIncidentCategory);
                return Ok(JsonIncidentCategory);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddIncidentCategory(ViewModel.tblIncidentCategory NewIncidentCategory)
        {
            try
            {
                bool ret = BisIncidentCategory.AddIncidentCategory(NewIncidentCategory);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateIncidentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblIncidentCategory UpdateIncidentCategory = JsonObject.ToObject<ViewModel.tblIncidentCategory>();
                bool ret = BisIncidentCategory.UpdateIncidentCategory(UpdateIncidentCategory);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteIncidentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblIncidentCategory DelIncidentCategory = JsonObject.ToObject<ViewModel.tblIncidentCategory>();
                bool ret = BisIncidentCategory.DeleteIncidentCategory(DelIncidentCategory);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}



