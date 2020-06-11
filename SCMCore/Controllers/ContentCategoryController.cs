using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class ContentCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentCategoryMethod BisContentCategory = new Bis.ContentCategoryMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillContentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search ContentCategorySearch = new ViewModel.Search();
                ContentCategorySearch.Order = " order by tblContentCategory.[Sort] Asc";
                ContentCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContentCategory = BisContentCategory.GetContentCategoryJsonData(ContentCategorySearch);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillArticleCategoryData()
        {
            try
            {
                ViewModel.Search ContentCategorySearch = new ViewModel.Search();
                ContentCategorySearch.Filter = " AND tblContentCategoryType.Name_En = 'Articles' AND tblContentCategory.IDParent='" + Guid.Empty + "'";
                ContentCategorySearch.Order = " order by tblContentCategory.[Sort]";
                ContentCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContentCategory = BisContentCategory.GetContentCategoryJsonData(ContentCategorySearch);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContentCategory NewContentCategory = JsonObject.ToObject<ViewModel.tblContentCategory>();
                bool ret = BisContentCategory.AddContentCategory(NewContentCategory);
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
        public IHttpActionResult UpdateContentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContentCategory UpdateContentCategory = JsonObject.ToObject<ViewModel.tblContentCategory>();
                bool ret = BisContentCategory.UpdateContentCategory(UpdateContentCategory);
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
        public IHttpActionResult DeleteContentCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContentCategory DelContentCategory = JsonObject.ToObject<ViewModel.tblContentCategory>();
                bool ret = BisContentCategory.DeleteContentCategory(DelContentCategory);
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