using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using System.Net;

namespace SCMCore.Controllers
{
    public class MaterialListItemController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.MaterialListItemMethod BisMaterialListItem = new Bis.MaterialListItemMethod();

        [HttpPost,CheckReferrerDomain]
        public IHttpActionResult GetMaterialListItemByIDXMaterialList(ViewModelSite.MaterialListItem obj)
        {
            try
            {
                JArray JsonMaterialListItem = BisMaterialListItem.GetMaterialListItemByIDXMaterialList(obj);
                return Ok(JsonMaterialListItem);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddMaterialListItem(ViewModelSite.MaterialListItem obj)
        {
            try
            {
                if (obj.IDLogUser != Guid.Empty)
                {
                    obj.IDLogUser = null;
                    bool ret = BisMaterialListItem.AddMaterialListItem(obj);
                    if (ret)
                    {
                        return Ok(ret);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return Content(HttpStatusCode.MethodNotAllowed, "ابتدا به حساب کاربری خود وارد شوید");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateMaterialListItem(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblMaterialListItem UpdateMaterialListItem = JsonObject.ToObject<ViewModel.tblMaterialListItem>();
                bool ret = BisMaterialListItem.UpdateMaterialListItem(UpdateMaterialListItem);
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
        public IHttpActionResult DeleteMaterialListItem(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblMaterialListItem DelMaterialListItem = JsonObject.ToObject<ViewModel.tblMaterialListItem>();
                bool ret = BisMaterialListItem.DeleteMaterialListItem(DelMaterialListItem);
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



