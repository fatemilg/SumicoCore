using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class PreparationPriceListController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddPreparationPriceList(object obj)
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();

            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JsonObject.Add("IDPersonel", AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()));
                ViewModel.tblPreparationPriceList Add = JsonObject.ToObject<ViewModel.tblPreparationPriceList>();
                bool ret = BisPreparationPriceList.AddPreparationPriceList(Add);
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
        public IHttpActionResult UpdatePreparationPriceList(object obj)
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();

            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JsonObject.Add("IDPersonel", AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()));
                ViewModel.tblPreparationPriceList update = JsonObject.ToObject<ViewModel.tblPreparationPriceList>();
                bool ret = BisPreparationPriceList.UpdatePreparationPriceList(update);
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
        public IHttpActionResult UpdatePreparationPriceListByShow(object obj)
        {
            Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceList update = JsonObject.ToObject<ViewModel.tblPreparationPriceList>();
                bool ret = BisPreparationPriceList.UpdatePreparationPriceListByShow(update);
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
        public IHttpActionResult GetPreparationPriceList(object obj)
        {
            try
            {

                Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();
                ViewModel.tblPreparationPriceList get = new ViewModel.tblPreparationPriceList();

                JObject JsonObject = JObject.Parse(obj.ToString());
                get.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonPreparationPriceList = BisPreparationPriceList.GetPreparationPriceList(get);
                return Ok(JsonPreparationPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeletePreparationPriceList(object obj)
        {
            try
            {

                Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPreparationPriceList del = JsonObject.ToObject<ViewModel.tblPreparationPriceList>();
                bool ret = BisPreparationPriceList.DeletePreparationPriceList(del);
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
