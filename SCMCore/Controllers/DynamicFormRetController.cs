using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class DynamicFormRetController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.DynamicFormRetMethod BisDynamicFormRet = new Bis.DynamicFormRetMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDynamicFormRet()
        {
            try
            {
                ViewModel.Search DynamicFormRetSearch = new ViewModel.Search();
                DynamicFormRetSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonDynamicFormRet = BisDynamicFormRet.GetDynamicFormRetJsonData(DynamicFormRetSearch);
                return Ok(JsonDynamicFormRet);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddDynamicFormRet(ViewModel.tblDynamicFormRet NewDynamicFormRet)
        {
            try
            {
                bool ret = BisDynamicFormRet.AddDynamicFormRet(NewDynamicFormRet);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateDynamicFormRet(ViewModel.tblDynamicFormRet UpdateDynamicFormRet)
        {
            try
            {
                bool ret = BisDynamicFormRet.UpdateDynamicFormRet(UpdateDynamicFormRet);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteDynamicFormRet(ViewModel.tblDynamicFormRet DelDynamicFormRet)
        {
            try
            {
                bool ret = BisDynamicFormRet.DeleteDynamicFormRet(DelDynamicFormRet);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetAllDataForSiteByIDX(ViewModel.tblDynamicFormRet DelDynamicFormRet)
        {
            try
            {
                JArray Objret = BisDynamicFormRet.GetAllDataForSiteByIDX(DelDynamicFormRet);
                return Ok(Objret);

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}



