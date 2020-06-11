using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{
    public class PreparationPriceListDetailController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPreparationPriceListDetailWithOutCategory(object obj)
        {
            try
            {

                Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
                ViewModel.tblPreparationPriceListDetail get = new ViewModel.tblPreparationPriceListDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                get.IDPreparationPriceList = JsonObject["IDPreparationPriceList"].ToString().StringToGuid();
                JArray JsonPreparationPriceList = BisPreparationPriceListDetail.GetPreparationPriceListDetailWithOutCategory(get);
                return Ok(JsonPreparationPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPreparationPriceListDetailByCategory(object obj)
        {
            try
            {

                Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
                ViewModel.tblPreparationPriceListDetail get = new ViewModel.tblPreparationPriceListDetail();

                JObject JsonObject = JObject.Parse(obj.ToString());
                get.IDPreparationPriceList = JsonObject["IDPreparationPriceList"].ToString().StringToGuid();
                JArray JsonPreparationPriceList = BisPreparationPriceListDetail.GetPreparationPriceListDetailByCategory(get);
                return Ok(JsonPreparationPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateEndUserPriceByIDPreparationPriceListDetail(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.UpdateEndUserPriceByIDPreparationPriceListDetail(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateQoutationDetainInPreparationPriceListDetail(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.UpdateQoutationDetainInPreparationPriceListDetail(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult IncreaseAllEndUserPriceForWithOutCategory(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.IncreaseAllEndUserPriceForWithOutCategory(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DecreaseAllEndUserPriceForWithOutCategory(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.DecreaseAllEndUserPriceForWithOutCategory(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult IncreaseAllEndUserPriceForByCategory(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.IncreaseAllEndUserPriceForByCategory(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DecreaseAllEndUserPriceForByCategory(object obj)
        {
            Bis.PreparationPriceListDetailMethod BisPreparationPriceListDetail = new Bis.PreparationPriceListDetailMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceListDetail update = JsonObject.ToObject<ViewModel.tblPreparationPriceListDetail>();
                bool ret = BisPreparationPriceListDetail.DecreaseAllEndUserPriceForByCategory(update);
                if (ret)
                {
                    return Ok(ret);

                }
                else
                {
                    return NotFound();

                }
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
