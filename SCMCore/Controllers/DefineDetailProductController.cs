using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class DefineDetailProductController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeSortInCrmForDefineDetailProduct(object obj)
        {
            try
            {

                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.tblDefineDetailProduct update = new ViewModel.tblDefineDetailProduct();
                update.JsonDefineDetailProduct = obj.ToString();
                bool ret = BisDefineDetailProduct.UpdateSortInCrm(update);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillDefineDetailProductByIDXProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.tblDefineDetailProduct DefineDetailProduct = new ViewModel.tblDefineDetailProduct();
                DefineDetailProduct.IDXProduct = JsonObject["IDXProduct"].ToString().StringToInt();
                JArray JsonDefine = BisDefineDetailProduct.GetGroupByPropertyJsonData(DefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillRulePropertyProductByIDXDefineDetailProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.tblDefineDetailProduct get = new ViewModel.tblDefineDetailProduct();
                get.IDX = JsonObject["IDXDefineDetailProduct"].ToString().StringToInt();
                JArray JsonContentCategory = BisDefineDetailProduct.GetJsonPropertyNameValueDataByIDXDefine(get);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetGeneratedJsonByIDProduct(ViewModel.tblDefineDetailProduct obj)
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JArray JSon = BisDefineDetailProduct.GetGeneratedJsonByIDProduct(obj);
                return Ok(JSon);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillDefineDetailProductByIDX(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.Search SearchDefineDetailProduct = new ViewModel.Search();
                SearchDefineDetailProduct.Filter = " AND tblDefineDetailProduct.IDX = " + JsonObject["IDX"].ToString().StringToInt().ToString();
                SearchDefineDetailProduct.JsonResult = " FOR JSON PATH";
                JArray JsonDefine = BisDefineDetailProduct.GetJsonDefineDetailProductData(SearchDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDefineDetailProductByGeneratedCode(ViewModel.tblDefineDetailProduct obj)
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JArray JsonDefine = BisDefineDetailProduct.GetDefineDetailProductByGeneratedCode(obj);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillAllUnderConstractionDefineDetailProduct()
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.Search SearchDefineDetailProduct = new ViewModel.Search();
                SearchDefineDetailProduct.Filter = " AND tblDefineDetailProduct.UnderSpot = 'true'";
                SearchDefineDetailProduct.JsonResult = " FOR JSON PATH";
                JArray JsonDefine = BisDefineDetailProduct.GetJsonDefineDetailProductData(SearchDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetListOfPartNumbers()
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.Search SearchDefineDetailProduct = new ViewModel.Search();
                SearchDefineDetailProduct.JsonResult = " FOR JSON PATH";
                JArray JsonDefine = BisDefineDetailProduct.GetListOfPartNumbers(SearchDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCompareListDetails(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDefineDetailProduct GetDefineDetailProduct = JsonObject.ToObject<ViewModel.tblDefineDetailProduct>();

                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JArray JsonDefine = BisDefineDetailProduct.GetCompareListDetail(GetDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPropertiesDetailForCompare(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDefineDetailProduct GetDefineDetailProduct = JsonObject.ToObject<ViewModel.tblDefineDetailProduct>();

                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JArray JsonDefine = BisDefineDetailProduct.GetPropertiesDetailForCompare(GetDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTechnicalPropertiesDetailForCompare(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDefineDetailProduct GetDefineDetailProduct = JsonObject.ToObject<ViewModel.tblDefineDetailProduct>();

                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JArray JsonDefine = BisDefineDetailProduct.GetTechnicalPropertiesDetailForCompare(GetDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDefineDetailProduct(object obj)   //Local>PurchaseOrderFile
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search SearchDefineDetailProduct = new ViewModel.Search();
                SearchDefineDetailProduct.Filter = " AND tblDefineDetailProduct.IDDefineDetailProduct = '" + JsonObject["IDDefineDetailProduct"].ToString() + "'";
                SearchDefineDetailProduct.JsonResult = " FOR JSON PATH";
                JArray JsonDefine = BisDefineDetailProduct.GetJsonDefineDetailProductData(SearchDefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetAutoCompleteDefineDetailProduct()
        {
            try
            {
                Bis.DefineDetailProductMethod BisDefineDetailProduct = new Bis.DefineDetailProductMethod();
                ViewModel.tblDefineDetailProduct DefineDetailProduct = new ViewModel.tblDefineDetailProduct();
                JArray JsonDefine = BisDefineDetailProduct.GetJsonAutoCompleteDefineDetailProduct(DefineDetailProduct);
                return Ok(JsonDefine);
            }
            catch
            {
                return NotFound();
            }

        }
    }

}
