using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class GalleryCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.GalleryCategoryMethod BisGalleryCategory = new Bis.GalleryCategoryMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetGalleryCategory()
        {
            try
            {
                
                ViewModel.Search GalleryCategorySearch = new ViewModel.Search();
                GalleryCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonGalleryCategory = BisGalleryCategory.GetGalleryCategoryJsonData(GalleryCategorySearch);
                return Ok(JsonGalleryCategory);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddGalleryCategory(ViewModel.tblGalleryCategory obj)
        {
            try
            {
                bool ret = BisGalleryCategory.AddGalleryCategory(obj);
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
        public IHttpActionResult UpdateGalleryCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblGalleryCategory UpdateGalleryCategory = JsonObject.ToObject<ViewModel.tblGalleryCategory>();
                bool ret = BisGalleryCategory.UpdateGalleryCategory(UpdateGalleryCategory);
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
        public IHttpActionResult DeleteGalleryCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblGalleryCategory DelGalleryCategory = JsonObject.ToObject<ViewModel.tblGalleryCategory>();
                bool ret = BisGalleryCategory.DeleteGalleryCategory(DelGalleryCategory);
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



