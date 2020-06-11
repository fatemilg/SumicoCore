using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class ContentRelateController : ApiController
    {
        Bis.ContentRelationMethod BisContentRelate = new Bis.ContentRelationMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetContentRelateDataByIDContent(ViewModel.tblContentRelate Obj)
        {
            try
            {
                JArray JsonContent = BisContentRelate.GetContentRelateDataByIDContent(Obj);
                return Ok(JsonContent);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContentRelate(ViewModel.tblContentRelate obj)
        {
            try
            {
                bool ret = BisContentRelate.AddContentRelate(obj);
                if (ret)
                {
                    return Ok();
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
        public IHttpActionResult DeleteContentRelate(ViewModel.tblContentRelate obj)
        {
            try
            {
                bool ret = BisContentRelate.DeleteContentRelate(obj);
                if (ret)
                {
                    return Ok();
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