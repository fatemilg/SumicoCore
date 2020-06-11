using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{
    public class AccessoryProductController : ApiController
    {
        Bis.AccessoryProductMethod BisAccessoryProduct = new Bis.AccessoryProductMethod();
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillAccessoryProductByIDXDefineDetailProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search SearchAccessoryProduct = new ViewModel.Search();
                SearchAccessoryProduct.Filter = " and Product.IDX = '" + JsonObject["IDXDefineDetailProduct"].ToString() + "' ";
                SearchAccessoryProduct.JsonResult = " FOR JSON Path";
                JArray JsonAccessoryProduct = BisAccessoryProduct.GetAccessoryProductJsonJsonData(SearchAccessoryProduct);
                return Ok(JsonAccessoryProduct);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}