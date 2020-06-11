using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class PeyvastPriceDetailController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPeyvastPriceDetail(object obj)
        {
            try
            {

                Bis.PeyvastPriceDetailMethod BisPeyvastPriceDetail = new Bis.PeyvastPriceDetailMethod();
                ViewModel.tblPeyvastPriceDetail getPeyvastPriceDetail = new ViewModel.tblPeyvastPriceDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPeyvastPriceDetail.IDPeyvastPriceFile = JsonObject["IDPeyvastPriceFile"].ToString().StringToGuid();
                JArray JsonPeyvastPriceDetail = BisPeyvastPriceDetail.GetPeyvastPriceDetail(getPeyvastPriceDetail);
                return Ok(JsonPeyvastPriceDetail);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSumsColumnsOfPeyvastPriceDetail(object obj)
        {
            try
            {

                Bis.PeyvastPriceDetailMethod BisPeyvastPriceDetail = new Bis.PeyvastPriceDetailMethod();
                ViewModel.tblPeyvastPriceDetail getPeyvastPriceDetail = new ViewModel.tblPeyvastPriceDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPeyvastPriceDetail.IDPeyvastPriceFile = JsonObject["IDPeyvastPriceFile"].ToString().StringToGuid();
                JArray JsonSupplierPriceList = BisPeyvastPriceDetail.GetSumsColumnsOfPeyvastPriceDetail(getPeyvastPriceDetail);
                return Ok(JsonSupplierPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

    }
}
