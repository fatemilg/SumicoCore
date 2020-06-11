using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class ContentDictionaryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentDictionaryMethod BisContentDictionary = new Bis.ContentDictionaryMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentDictionaryByIDContent(ViewModel.tblContentDictionary obj)
        {
            try
            {
                JArray JsonContentDictionary = BisContentDictionary.GetContentDictionaryByIDContentJsonData(obj);
                return Ok(JsonContentDictionary);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSelectedDataByIDContent(ViewModel.tblContentDictionary obj)
        {
            try
            {
                JArray JsonContentDictionary = BisContentDictionary.GetSelectedDataByIDContent(obj);
                return Ok(JsonContentDictionary);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContentDictionary(ViewModel.tblContentDictionary obj)
        {
            try
            {
                bool ret = BisContentDictionary.AddContentDictionary(obj);
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
        public IHttpActionResult ChangeSortInContentDictionary(ViewModel.tblContentDictionary obj)
        {
            try
            {
                bool ret = BisContentDictionary.ChangeSortInContentDictionary(obj);
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
        public IHttpActionResult DeleteContentDictionary(ViewModel.tblContentDictionary obj)
        {
            try
            {
                bool ret = BisContentDictionary.DeleteContentDictionary(obj);
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



