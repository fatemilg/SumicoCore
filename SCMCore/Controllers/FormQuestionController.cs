using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class FormQuestionController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.FormQuestionMethod BisFormQuestion = new Bis.FormQuestionMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetFormQuestionDataByIDDynamicForm(ViewModel.tblFormQuestion ObjFormQuestion)
        {
            try
            {
                JArray JsonFormQuestion = BisFormQuestion.GetFormQuestionDataByIDDynamicForm(ObjFormQuestion);
                return Ok(JsonFormQuestion);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddFormQuestion(ViewModel.tblFormQuestion NewFormQuestion)
        {
            try
            {
                bool ret = BisFormQuestion.AddFormQuestion(NewFormQuestion);
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
        public IHttpActionResult UpdateFormQuestion(ViewModel.tblFormQuestion UpdateFormQuestion)
        {
            try
            {
                bool ret = BisFormQuestion.UpdateFormQuestion(UpdateFormQuestion);
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
        public IHttpActionResult DeleteFormQuestion(ViewModel.tblFormQuestion DelFormQuestion)
        {
            try
            {
                bool ret = BisFormQuestion.DeleteFormQuestion(DelFormQuestion);
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
        public IHttpActionResult ChangeSortQuestions(ViewModel.tblFormQuestion obj)
        {
            try
            {
                bool ret = BisFormQuestion.ChangeSortQuestions(obj);
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

