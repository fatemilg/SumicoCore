using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class RelatePurchaseOrderFileController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddRelatePurchaseOrderFile(object obj)
        {
            try
            {
                Bis.RelatePurchaseOrderFileMethod bisRelatePurchaseOrderFile = new Bis.RelatePurchaseOrderFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                Guid IDRegister = AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid());
                ViewModel.tblRelatePurchaseOrderFile Add = JsonObject.ToObject<ViewModel.tblRelatePurchaseOrderFile>();
                Add.IDRegister = IDRegister;
                bool ret = bisRelatePurchaseOrderFile.AddRelatePurchaseOrderFile(Add);
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
        public IHttpActionResult GetDataForPOHistory(object obj)
        {
            try
            {
                Bis.RelatePurchaseOrderFileMethod bisRelatePurchaseOrderFile = new Bis.RelatePurchaseOrderFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblRelatePurchaseOrderFile get = new ViewModel.tblRelatePurchaseOrderFile();
                get.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonRelatePurchaseOrderFile = bisRelatePurchaseOrderFile.GetDataForPOHistory(get);
                return Ok(JsonRelatePurchaseOrderFile);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
