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
    public class TechnicalPropertiesController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillTechnicalPropertiesByIDXDefineDetailProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.DetailTechnicalPropertyMethod BisDetailTechnicalProperty = new Bis.DetailTechnicalPropertyMethod();
                ViewModel.Search DetailTechnicalPropertySearch = new ViewModel.Search();
                DetailTechnicalPropertySearch.Filter = " and tblDefineDetailProduct.IDX = '" + JsonObject["IDX"].ToString().StringToInt().ToString() + "'";
                DetailTechnicalPropertySearch.Order = " order by tblDetailTechnicalProperty.[Order] asc";
                DetailTechnicalPropertySearch.JsonResult = " FOR JSON PATH";
                JArray JsonDetailTechnicalProperty = BisDetailTechnicalProperty.GetJsonDetailTechnicalPropertyData(DetailTechnicalPropertySearch);
                return Ok(JsonDetailTechnicalProperty);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}