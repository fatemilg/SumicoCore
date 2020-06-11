using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class LevelOfPersonelInCompanyController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.LevelOfPersonelInCompanyMethod BisLevelOfPersonelInCompany = new Bis.LevelOfPersonelInCompanyMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetLevelOfPersonelInCompany()
        {
            try
            {
                ViewModel.Search LevelOfPersonelInCompanySearch = new ViewModel.Search();
                LevelOfPersonelInCompanySearch.Order = " ORDER BY tblLevelOfPersonelInCompany.Sort ";
                LevelOfPersonelInCompanySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonLevelOfPersonelInCompany = BisLevelOfPersonelInCompany.GetLevelOfPersonelInCompanyJsonData(LevelOfPersonelInCompanySearch);
                return Ok(JsonLevelOfPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddLevelOfPersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblLevelOfPersonelInCompany NewLevelOfPersonelInCompany = JsonObject.ToObject<ViewModel.tblLevelOfPersonelInCompany>();
                bool ret = BisLevelOfPersonelInCompany.AddLevelOfPersonelInCompany(NewLevelOfPersonelInCompany);
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
        public IHttpActionResult UpdateLevelOfPersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblLevelOfPersonelInCompany UpdateLevelOfPersonelInCompany = JsonObject.ToObject<ViewModel.tblLevelOfPersonelInCompany>();
                bool ret = BisLevelOfPersonelInCompany.UpdateLevelOfPersonelInCompany(UpdateLevelOfPersonelInCompany);
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
        public IHttpActionResult DeleteLevelOfPersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblLevelOfPersonelInCompany DelLevelOfPersonelInCompany = JsonObject.ToObject<ViewModel.tblLevelOfPersonelInCompany>();
                bool ret = BisLevelOfPersonelInCompany.DeleteLevelOfPersonelInCompany(DelLevelOfPersonelInCompany);
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



