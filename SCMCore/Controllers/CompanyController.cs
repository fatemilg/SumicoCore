using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class CompanyController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.CompanyMethod BisCompany = new Bis.CompanyMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCompany()
        {
            try
            {
                ViewModel.Search CompanySearch = new ViewModel.Search();
                CompanySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonCompany = BisCompany.GetCompanyJsonData(CompanySearch);
                return Ok(JsonCompany);
            }
            catch
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblCompany NewCompany = JsonObject.ToObject<ViewModel.tblCompany>();
                bool ret = BisCompany.AddCompany(NewCompany);
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
        public IHttpActionResult UpdateCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblCompany UpdateCompany = JsonObject.ToObject<ViewModel.tblCompany>();
                bool ret = BisCompany.UpdateCompany(UpdateCompany);
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
        public IHttpActionResult DeleteCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblCompany DelCompany = JsonObject.ToObject<ViewModel.tblCompany>();
                bool ret = BisCompany.DeleteCompany(DelCompany);
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



