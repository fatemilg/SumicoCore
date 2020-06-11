
using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class UserGroupController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.GroupMethod BisUserGroup = new Bis.GroupMethod();

        [HttpPost,CheckReferrerDomain]
        public IHttpActionResult GetUserGroupByGroupType(ViewModel.tblGroupType obj)
        {
            try
            {
                ViewModel.tblGroupType UserGroupSearch = new ViewModel.tblGroupType();
                UserGroupSearch.UnicName = obj.UnicName;
                JArray JsonUserGroup = BisUserGroup.GetUserGroupByGroupTypeJsonData(UserGroupSearch);
                return Ok(JsonUserGroup);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        
        
    }
}



