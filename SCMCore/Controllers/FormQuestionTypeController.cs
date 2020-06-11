using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class FormQuestionTypeController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.FormQuestionTypeMethod BisFormQuestionType = new Bis.FormQuestionTypeMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetFormQuestionType()
        {
            try
            {
                ViewModel.tblFormQuestionType ObjFormQuestionType = new ViewModel.tblFormQuestionType();
                JArray JsonFormQuestionType = BisFormQuestionType.GetFormQuestionTypeJsonData(ObjFormQuestionType);
                return Ok(JsonFormQuestionType);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddFormQuestionType(ViewModel.tblFormQuestionType NewFormQuestionType)
        {
            try
            {
                bool ret = BisFormQuestionType.AddFormQuestionType(NewFormQuestionType);
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
        public IHttpActionResult UpdateFormQuestionType(ViewModel.tblFormQuestionType UpdateFormQuestionType)
        {
            try
            {
                bool ret = BisFormQuestionType.UpdateFormQuestionType(UpdateFormQuestionType);
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
        public IHttpActionResult DeleteFormQuestionType(ViewModel.tblFormQuestionType DelFormQuestionType)
        {
            try
            {
                bool ret = BisFormQuestionType.DeleteFormQuestionType(DelFormQuestionType);
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
    }
}



