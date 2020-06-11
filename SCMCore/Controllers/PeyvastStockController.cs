using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Controllers
{
    public class PeyvastStockController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPeyvastStock(object obj)
        {
            try
            {
                Bis.PeyvastStockMethod BisPeyvastStock = new Bis.PeyvastStockMethod();
                ViewModel.tblPeyvastStock getPeyvastStock = new ViewModel.tblPeyvastStock();
                JArray JsonPeyvastStock = BisPeyvastStock.GetPeyvastStockData(getPeyvastStock);
                return Ok(JsonPeyvastStock);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
