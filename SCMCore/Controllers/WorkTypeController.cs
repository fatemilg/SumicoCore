using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class WorkTypeController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.WorkTypeMethod BisWorkType = new Bis.WorkTypeMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetWorkType()
        {
            try
            {
                ViewModel.Search WorkTypeSearch = new ViewModel.Search();
                WorkTypeSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonWorkType = BisWorkType.GetWorkTypeJsonData(WorkTypeSearch);
                return Ok(JsonWorkType);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddWorkType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblWorkType NewWorkType = JsonObject.ToObject<ViewModel.tblWorkType>();
                bool ret = BisWorkType.AddWorkType(NewWorkType);
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
        public IHttpActionResult UpdateWorkType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblWorkType UpdateWorkType = JsonObject.ToObject<ViewModel.tblWorkType>();
                bool ret = BisWorkType.UpdateWorkType(UpdateWorkType);
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
        public IHttpActionResult DeleteWorkType(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblWorkType DelWorkType = JsonObject.ToObject<ViewModel.tblWorkType>();
                bool ret = BisWorkType.DeleteWorkType(DelWorkType);
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



