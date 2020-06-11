using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class MaterialListController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.MaterialListMethod BisMaterialList = new Bis.MaterialListMethod();


        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetMaterialListByIDLogUser(ViewModelSite.tblMaterialList obj)
        {
            try
            {
                obj.IDUser = AuUser.ReturnIDUser(obj.IDLogUser);
                if (obj.IDUser != Guid.Empty)
                {
                    obj.IDLogUser = null;
                    JArray JsonMaterialList = BisMaterialList.GetMaterialListJsonDataByIDLogUser(obj);
                    return Ok(JsonMaterialList);
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
        public IHttpActionResult AddMaterialList(ViewModelSite.tblMaterialList obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    obj.IDUser = AuUser.ReturnIDUser(obj.IDLogUser);
                    if (obj.IDUser != Guid.Empty)
                    {
                        obj.IDLogUser = null;
                        bool ret = BisMaterialList.AddMaterialListInSite(obj);
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
                else
                {
                    PublicFunctions getError = new PublicFunctions();
                    return Content(HttpStatusCode.NotFound, getError.GetErrorListFromModelState(ModelState));
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateMaterialList(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblMaterialList UpdateMaterialList = JsonObject.ToObject<ViewModel.tblMaterialList>();
                bool ret = BisMaterialList.UpdateMaterialList(UpdateMaterialList);
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
        public IHttpActionResult DeleteMaterialList(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblMaterialList DelMaterialList = JsonObject.ToObject<ViewModel.tblMaterialList>();
                bool ret = BisMaterialList.DeleteMaterialList(DelMaterialList);
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



