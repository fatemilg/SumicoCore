using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using VMSite = SCMCore.ViewModelSite;

namespace SCMCore.Controllers
{
    public class UserSiteController : ApiController
    {
        Bis.UserSiteMethod BisUserSite = new Bis.UserSiteMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetUserSite()
        {
            try
            {
                ViewModel.tblUserSite userSite = new ViewModel.tblUserSite();
                JArray JsonUserSite = BisUserSite.GetUserSiteJson(userSite);
                return Ok(JsonUserSite);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIntegratedUserSiteByID(ViewModel.tblUserSite UserSite)
        {
            try
            {
                JArray JsonPersonelInCompany = BisUserSite.GetIntegratedUserSiteByID(UserSite);
                return Ok(JsonPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult LoginUserSite(VMSite.SignIn obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.Password = obj.Password.FixFarsi().EncryptData();
                    JArray jsLogin = BisUserSite.LoginUserSite(obj);
                    return Ok(jsLogin);
                }
                catch
                {
                    return NotFound();
                }
            }

            else
            {
                PublicFunctions getError = new PublicFunctions();
                return Content(HttpStatusCode.NotFound, getError.GetErrorListFromModelState(ModelState));
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult LoginUserSiteByToken(ViewModel.tblLogUser obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    JArray jsLogin = BisUserSite.LoginUserSiteByToken(obj);
                    return Ok(jsLogin);
                }
                catch
                {
                    return NotFound();
                }
            }

            else
            {
                PublicFunctions getError = new PublicFunctions();
                return Content(HttpStatusCode.NotFound, getError.GetErrorListFromModelState(ModelState));
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCountNewUserSiteNotShown()
        {
            try
            {
                ViewModel.tblUserSite userSite = new ViewModel.tblUserSite();
                JArray JsonUserSite = BisUserSite.GetCountNewUserNotShown(userSite);
                return Ok(JsonUserSite);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetUserSiteByNationalCode(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblUserSite userSite = JsonObject.ToObject<ViewModel.tblUserSite>();
                JArray JsonUserSite = BisUserSite.GetUserSiteJsonDataByNationalCode(userSite);
                return Ok(JsonUserSite);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult CheckPersonelInCompanyExistInUserSite(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblUserSite userSite = JsonObject.ToObject<ViewModel.tblUserSite>();
                JArray JsonUserSite = BisUserSite.CheckPersonelInCompanyExistInUserSite(userSite);
                return Ok(JsonUserSite);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddUserSiteByEnroll(VMSite.SignUp obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.Password = obj.Password.FixFarsi().EncryptData();
                    bool ret = BisUserSite.AddUserSiteByEnroll(obj);
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
            else
            {
                PublicFunctions getError = new PublicFunctions();
                return Content(HttpStatusCode.NotFound, getError.GetErrorListFromModelState(ModelState));
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddForDynamicForm(VMSite.UserSiteDynamicForm obj)
        {
                try
                {
                    bool ret = BisUserSite.AddForDynamicForm(obj);
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
        public IHttpActionResult AddUserSiteIntoPersonelInCompany(ViewModel.tblUserSite obj)
        {
            try
            {
                bool ret = BisUserSite.AddUserSiteIntoPersonelInCompany(obj);
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
        public IHttpActionResult UpdateShowDate(ViewModel.tblUserSite UserSite)
        {
            try
            {
                bool ret = BisUserSite.UpdateShowDate(UserSite);
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
        public IHttpActionResult UpdaIDPersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblUserSite userSite = JsonObject.ToObject<ViewModel.tblUserSite>();
                bool ret = BisUserSite.UpdateIDPersonelInCompany(userSite);
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
