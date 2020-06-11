using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{

    public class MenuController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        // GET api/menu
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillMenu(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.AccessLevelMethod BisAccessLevel = new Bis.AccessLevelMethod();
                ViewModel.Search AccessSearch = new ViewModel.Search();
                AccessSearch.Filter = " And IDUser = '" + AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()) + "'  AND tblAccessLevel.Access='True' AND tblMenu.ParentID = '" + Guid.Empty.ToString() + "' ";
                AccessSearch.Order = " order by tblMenu.[Order] Asc";
                AccessSearch.JsonResult = " FOR JSON AUTO";
                JArray JsonAccessLevel = BisAccessLevel.GetJsonAccessLevelDataForTree(AccessSearch);
                return Ok(JsonAccessLevel);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillSubMenu(object ParentMenu)
        {
            try
            {
                JObject JsonObject = JObject.Parse(ParentMenu.ToString());
                Bis.AccessLevelMethod BisAccessLevel = new Bis.AccessLevelMethod();
                ViewModel.Search AccessSearch = new ViewModel.Search();
                AccessSearch.Filter = " And IDUser = '" + AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()) + "'  AND tblAccessLevel.Access='True' AND tblMenu.ParentID = '" + JsonObject["IDParent"].ToString() + "'";
                AccessSearch.Order = " order by tblMenu.[Order] Asc";
                AccessSearch.JsonResult = " FOR JSON AUTO";
                JArray JsonAccessLevel = BisAccessLevel.GetJsonAccessLevelDataForTree(AccessSearch);
                return Ok(JsonAccessLevel);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPersonelOfMenuData(object ParentMenu)
        {
            try
            {
                JObject JsonObject = JObject.Parse(ParentMenu.ToString());
                Bis.AccessLevelMethod BisAccessLevel = new Bis.AccessLevelMethod();
                ViewModel.tblAccessLevel GetAccessLevel = JsonObject.ToObject<ViewModel.tblAccessLevel>();
                GetAccessLevel.MenuUrl = GetAccessLevel.MenuUrl.Replace(GetAccessLevel.MenuUrl.Split('/')[0] + "/" + GetAccessLevel.MenuUrl.Split('/')[1] + "/" + GetAccessLevel.MenuUrl.Split('/')[2] + "/", "");
                JArray PersonelOfMenu = BisAccessLevel.GetJsonDataForEventUser(GetAccessLevel);
                return Ok(PersonelOfMenu);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
