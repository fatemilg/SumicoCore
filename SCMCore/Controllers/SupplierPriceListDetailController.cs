using System;
using System.Collections.Generic;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class SupplierPriceListDetailController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSupplierPriceListDetail(object obj)
        {
            try
            {

                Bis.SupplierPriceListDetailMethod BisSupplierPriceListDetail = new Bis.SupplierPriceListDetailMethod();
                ViewModel.tblSupplierPriceListDetail getSupplierPriceListDetail = new ViewModel.tblSupplierPriceListDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getSupplierPriceListDetail.IDSupplierPriceListFile = JsonObject["IDSupplierPriceListFile"].ToString().StringToGuid();
                JArray JsonSupplierPriceList = BisSupplierPriceListDetail.GetSupplierPriceListDetail(getSupplierPriceListDetail);
                return Ok(JsonSupplierPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSumsColumnsOfSupplierPriceListDetail(object obj)
        {
            try
            {

                Bis.SupplierPriceListDetailMethod BisSupplierPriceListDetail = new Bis.SupplierPriceListDetailMethod();
                ViewModel.tblSupplierPriceListDetail getSupplierPriceListDetail = new ViewModel.tblSupplierPriceListDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getSupplierPriceListDetail.IDSupplierPriceListFile = JsonObject["IDSupplierPriceListFile"].ToString().StringToGuid();
                JArray JsonSupplierPriceList = BisSupplierPriceListDetail.GetSumsColumnsOfSupplierPriceListDetail(getSupplierPriceListDetail);
                return Ok(JsonSupplierPriceList);
            }
            catch
            {
                return NotFound();
            }

        }



    }
}
