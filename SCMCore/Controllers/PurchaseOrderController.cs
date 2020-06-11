using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class PurchaseOrderController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPurchaseOrderInProgress()
        {
            try
            {

                Bis.PurchaseOrderMethod BisPurchaseOrder = new Bis.PurchaseOrderMethod();
                ViewModel.Search getPurchaseOrder = new ViewModel.Search();
                getPurchaseOrder.JsonResult = " FOR JSON Path";
                JArray JsonPurchaseOrder = BisPurchaseOrder.GetPurchaseOrderInProgress(getPurchaseOrder);
                return Ok(JsonPurchaseOrder);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPurchaseOrderByIDPurchaseOrderFile(object obj)
        {
            try
            {

                Bis.PurchaseOrderMethod BisPurchaseOrder = new Bis.PurchaseOrderMethod();
                ViewModel.tblPurchaseOrder getPurchaseOrder = new ViewModel.tblPurchaseOrder();
                JObject JsonObject = JObject.Parse(obj.ToString());
                getPurchaseOrder.IDPurchaseOrderFile = JsonObject["IDPurchaseOrderFile"].ToString().StringToGuid();
                JArray JsonPurchaseOrder = BisPurchaseOrder.GetPurchaseOrderByIDPurchaseOrderFile(getPurchaseOrder);
                return Ok(JsonPurchaseOrder);
            }
            catch(Exception ex)
            {

                return NotFound();
            }

        }



       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPurchaseOrderByPartNumber(object obj)
        {
            try
            {

                Bis.PurchaseOrderMethod BisPurchaseOrder = new Bis.PurchaseOrderMethod();
                ViewModel.tblPurchaseOrder getPurchaseOrder = new ViewModel.tblPurchaseOrder();
                JObject JsonObject = JObject.Parse(obj.ToString());
                getPurchaseOrder.PartNumber = JsonObject["PartNumber"].ToString();
                JArray JsonPurchaseOrder = BisPurchaseOrder.GetPurchaseOrderByPartNumber(getPurchaseOrder);
                return Ok(JsonPurchaseOrder);
            }
            catch (Exception ex)
            {

                return NotFound();
            }

        }
    }
}
