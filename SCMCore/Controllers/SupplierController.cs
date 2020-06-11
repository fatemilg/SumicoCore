using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{

    public class SupplierController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.LegalUserMethod BisLegalUser = new Bis.LegalUserMethod();
        // GET api/Supplier
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillSupplier()
        {
            try
            {
                ViewModel.Search SearchLegalUser = new ViewModel.Search();
                SearchLegalUser.Order = " order by tblLegalUser.[Sort] Asc";
                SearchLegalUser.JsonResult = " FOR JSON Path";
                JArray JsonLegalUser = BisLegalUser.GetSupplierJsonData(SearchLegalUser);
                return Ok(JsonLegalUser);
            }
            catch
            {
                return NotFound();
            }

        }


    }
}
