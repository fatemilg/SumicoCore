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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class RelatedDefineDetailProductController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillRelatedDefineDetailProductDataByIDXDefineDetailProduct(object obj)
        {
            try
            {
                Bis.RelatedDefineDetailProductMethod BisRelatedDefineDetailProduct = new Bis.RelatedDefineDetailProductMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblRelatedDefineDetailProduct GetRelatedDefineDetailProduct = new ViewModel.tblRelatedDefineDetailProduct();
                GetRelatedDefineDetailProduct.IDXDefineDetailProduct = JsonObject["IDXDefineDetailProduct"].ToString().StringToInt();
                JArray JsonContentCategory = BisRelatedDefineDetailProduct.GetJsonAllRelations(GetRelatedDefineDetailProduct);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}