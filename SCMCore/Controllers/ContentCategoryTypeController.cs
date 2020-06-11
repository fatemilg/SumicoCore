using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class ContentCategoryTypeController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentCategoryTypeMethod BisContentCategoryType = new Bis.ContentCategoryTypeMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillContentCategoryTypeComplete()
        {
            try
            {
                ViewModel.Search ContentCategoryTypeSearch = new ViewModel.Search();
                ContentCategoryTypeSearch.Order = " order by tblContentCategoryContent.[Sort]";
                ContentCategoryTypeSearch.JsonResult = " FOR JSON Path ";
                JArray JsonContentCategoryType = BisContentCategoryType.GetCompleteContentCategoryTypeJsonData(ContentCategoryTypeSearch);
                return Ok(JsonContentCategoryType);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillArticleCategoryComplete()
        {
            try
            {
                ViewModel.Search ContentCategoryTypeSearch = new ViewModel.Search();
                ContentCategoryTypeSearch.Filter = " AND tblContentCategoryType.Name_En = 'Articles'";
                ContentCategoryTypeSearch.Order = " order by tblContentCategoryType.[Sort]";
                ContentCategoryTypeSearch.JsonResult = " FOR JSON Path ";
                JArray JsonContentCategoryType = BisContentCategoryType.GetCompleteContentCategoryTypeJsonData(ContentCategoryTypeSearch);
                return Ok(JsonContentCategoryType);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillNewsLetterCategoryComplete()
        {
            try
            {
                ViewModel.Search ContentCategoryTypeSearch = new ViewModel.Search();
                ContentCategoryTypeSearch.Filter = " AND tblContentCategoryType.Name_En = 'NewsLetter'";
                ContentCategoryTypeSearch.Order = " order by tblContentCategoryType.[Sort]";
                ContentCategoryTypeSearch.JsonResult = " FOR JSON Path ";
                JArray JsonContentCategoryType = BisContentCategoryType.GetCompleteContentCategoryTypeJsonData(ContentCategoryTypeSearch);
                return Ok(JsonContentCategoryType);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillContentCategoryType()
        {
            try
            {
                ViewModel.Search ContentCategoryTypeSearch = new ViewModel.Search();
                ContentCategoryTypeSearch.Order = " order by tblContentCategoryType.[Sort]";
                ContentCategoryTypeSearch.JsonResult = " FOR JSON Path ";
                JArray JsonContentCategoryType = BisContentCategoryType.GetContentCategoryTypeJsonData(ContentCategoryTypeSearch);
                return Ok(JsonContentCategoryType);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}