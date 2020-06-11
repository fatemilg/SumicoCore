using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class ContentCategoryContentController : ApiController
    {
        Bis.ContentCategoryContentMethod BisContentCategoryContent = new Bis.ContentCategoryContentMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContentCategoryContent(ViewModel.tblContentCategoryContent obj)
        {
            try
            {
                bool ret = BisContentCategoryContent.AddContentCategoryContent(obj);
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
        public IHttpActionResult DeleteContentCategoryContent(ViewModel.tblContentCategoryContent obj)
        {
            try
            {
                bool ret = BisContentCategoryContent.DeleteContentCategoryContent(obj);
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
        public IHttpActionResult UpdateSortContentCategoryContent(ViewModel.tblContentCategoryContent obj)
        {
            try
            {
                bool ret = BisContentCategoryContent.UpdateSortContentCategoryContent(obj);
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