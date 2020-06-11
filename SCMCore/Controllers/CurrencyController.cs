using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{
    public class CurrencyController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCurrency()
        {
            Bis.CurrencyMethod bisCurrency = new Bis.CurrencyMethod();
            try
            {
                ViewModel.Search get = new ViewModel.Search();
                get.Order = " order by Title ";
                get.JsonResult = " FOR JSON Path";
                JArray JsonLegalUser = bisCurrency.GetCurrencyData(get);
                return Ok(JsonLegalUser);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
