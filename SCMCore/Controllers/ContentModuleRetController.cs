
using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class ContentModuleRetController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentModuleRetMethod BisContentModuleRet = new Bis.ContentModuleRetMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentModuleByIDRet(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                JArray JsonContentModule = BisContentModuleRet.GetContentModuleByIDRetJsonData(obj);
                return Ok(JsonContentModule);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentModuleByUniqueName(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                JArray JsonContentModule = BisContentModuleRet.GetContentModuleByUniqueNameJsonData(obj);
                return Ok(JsonContentModule);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentModuleByIDContentModule(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                JArray JsonContentModule = BisContentModuleRet.GetContentModuleByIDContentModule(obj);
                return Ok(JsonContentModule);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentModuleByIDContentModule_ForTrainingCourseBatch(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                JArray JsonContentModule = BisContentModuleRet.GetContentModuleByIDContentModule_ForTrainingCourseBatch(obj);
                return Ok(JsonContentModule);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContentModuleRet(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                bool ret = BisContentModuleRet.AddContentModuleRet(obj);
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
        public IHttpActionResult UpdateContentModuleRet(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                bool ret = BisContentModuleRet.UpdateContentModuleRet(obj);
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
        public IHttpActionResult DeleteContentModuleRet(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                bool ret = BisContentModuleRet.DeleteContentModuleRet(obj);
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
        public IHttpActionResult UpdateSortContentModuleRet(ViewModel.tblContentModuleRet obj)
        {
            try
            {
                bool ret = BisContentModuleRet.UpdateSortContentModuleRet(obj);
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



