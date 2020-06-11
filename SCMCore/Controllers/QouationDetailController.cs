using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class QouationDetailController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQouationDetail(object obj)
        {
            try
            {

                Bis.QouationDetailMethod BisQouationDetail = new Bis.QouationDetailMethod();
                ViewModel.tblQouationDetail getQouationDetail = new ViewModel.tblQouationDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouationDetail.IDQouationFile = JsonObject["IDQouationFile"].ToString().StringToGuid();
                JArray JsonQouation = BisQouationDetail.GetQouationDetail(getQouationDetail);
                return Ok(JsonQouation);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSumsColumnsOfQouationDetail(object obj)
        {
            try
            {

                Bis.QouationDetailMethod BisQouationDetail = new Bis.QouationDetailMethod();
                ViewModel.tblQouationDetail getQouationDetail = new ViewModel.tblQouationDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouationDetail.IDQouationFile = JsonObject["IDQouationFile"].ToString().StringToGuid();
                JArray JsonQouation = BisQouationDetail.GetSumsColumnsOfQouationDetail(getQouationDetail);
                return Ok(JsonQouation);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddAllQouationDetail(object obj)
        {
            try
            {
                AuthorizationUser AuUser = new AuthorizationUser();

                Bis.QouationDetailMethod BisQouationDetail = new Bis.QouationDetailMethod();
                ViewModel.tblQouationDetail getQouationDetail = new ViewModel.tblQouationDetail();
                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouationDetail.AllDetailJson = JsonObject["AllDetailJson"].ToString();
                getQouationDetail.IDQouationFile = JsonObject["IDQouationFile"].ToString().StringToGuid();
                getQouationDetail.IDPersonel = AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid());
                getQouationDetail.RatioChfToEu = JsonObject["RatioChfToEu"].ToString().StringToDecimal();

                bool JsonQouation = BisQouationDetail.AddAllQouationDetail(getQouationDetail);
                return Ok(JsonQouation);
            }
            catch
            {
                return NotFound();
            }

        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteSelectedQouationDetail(object obj)
        {
            try
            {
                Bis.QouationDetailMethod BisQouationDetail = new Bis.QouationDetailMethod();
                ViewModel.tblQouationDetail getQouationDetail = new ViewModel.tblQouationDetail();
                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouationDetail.ItemsSelected = JsonObject["ItemsSelected"].ToString();
                bool JsonQouation = BisQouationDetail.DeleteSelectedQouationDetail(getQouationDetail);
                return Ok(JsonQouation);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQouationDetailByPartNumber(object obj)
        {
            try
            {

                Bis.QouationDetailMethod BisQouationDetail = new Bis.QouationDetailMethod();
                ViewModel.tblQouationDetail getQouationDetail = new ViewModel.tblQouationDetail();
                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouationDetail.PartNumber = JsonObject["PartNumber"].ToString();
                JArray JsonPurchaseOrder = BisQouationDetail.GetQouationDetailByPartNumber(getQouationDetail);
                return Ok(JsonPurchaseOrder);
            }
            catch (Exception ex)
            {

                return NotFound();
            }

        }

    }
}
