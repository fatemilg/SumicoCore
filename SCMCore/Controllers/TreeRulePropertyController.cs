using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class TreeRulePropertyController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TreeRulePropertyMethod BisTreeRuleProperty = new Bis.TreeRulePropertyMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTreeRulePropertyByIDProduct(ViewModel.tblTreeRuleProperty TreeRulePropertySearch)
        {
            try
            {
                JArray JsonTreeRuleProperty = BisTreeRuleProperty.GetTreeRulePropertyJsonDataByIDProduct(TreeRulePropertySearch);
                return Ok(JsonTreeRuleProperty);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTreeRuleProperty(ViewModel.tblTreeRuleProperty obj)
        {
            try
            {
                bool ret = BisTreeRuleProperty.AddTreeRuleProperty(obj);
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