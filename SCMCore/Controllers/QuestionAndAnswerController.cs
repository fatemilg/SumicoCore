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
using System.IO;

namespace SCMCore.Controllers
{
    public class QuestionAndAnswerController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.QuestionAndAnswerMethod BisQuestionAndAnswer = new Bis.QuestionAndAnswerMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetNewQuestionAnswer()
        {
            try
            {
                ViewModel.Search SearchQuestionAndAnswer = new ViewModel.Search();
                SearchQuestionAndAnswer.Filter = " and tblQuestionAndAnswer.Accept is NULL";
                SearchQuestionAndAnswer.Order = " ORDER BY tblQuestionAndAnswer.EditDate desc";
                SearchQuestionAndAnswer.JsonResult = " FOR JSON Path";
                JArray JsonQuestionAndAnswer = BisQuestionAndAnswer.GetQuestionAndAnswerForDefineDetailProductJsonData(SearchQuestionAndAnswer);
                return Ok(JsonQuestionAndAnswer);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDeniedQuestionAnswer()
        {
            try
            {
                ViewModel.Search SearchQuestionAndAnswer = new ViewModel.Search();
                SearchQuestionAndAnswer.Filter = " and tblQuestionAndAnswer.Accept=0";
                SearchQuestionAndAnswer.Order = " ORDER BY tblQuestionAndAnswer.EditDate desc";
                SearchQuestionAndAnswer.JsonResult = " FOR JSON Path";
                JArray JsonQuestionAndAnswer = BisQuestionAndAnswer.GetQuestionAndAnswerForDefineDetailProductJsonData(SearchQuestionAndAnswer);
                return Ok(JsonQuestionAndAnswer);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetAcceptedQuestionAnswer()
        {
            try
            {
                ViewModel.Search SearchQuestionAndAnswer = new ViewModel.Search();
                SearchQuestionAndAnswer.Filter = " and tblQuestionAndAnswer.Accept=1";
                SearchQuestionAndAnswer.Order = " ORDER BY tblQuestionAndAnswer.EditDate desc";
                SearchQuestionAndAnswer.JsonResult = " FOR JSON Path";
                JArray JsonQuestionAndAnswer = BisQuestionAndAnswer.GetQuestionAndAnswerForDefineDetailProductJsonData(SearchQuestionAndAnswer);
                return Ok(JsonQuestionAndAnswer);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillQuestionAndAnswerForDefineDetailProductByIDX(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search SearchQuestionAndAnswer = new ViewModel.Search();
                SearchQuestionAndAnswer.Filter = " AND tblQuestionAndAnswer.IDRet = (SELECT TOP 1 IDDefineDetailProduct FROM tblDefineDetailProduct WHERE IDX = " + JsonObject["IDX"].ToString() + ") AND tblQuestionAndAnswer.Accept=1";
                SearchQuestionAndAnswer.Order = " ORDER BY tblQuestionAndAnswer.[Sort]";
                SearchQuestionAndAnswer.JsonResult = " FOR JSON Path";
                JArray JsonQuestionAndAnswer = BisQuestionAndAnswer.GetQuestionAndAnswerForDefineDetailProductJsonData(SearchQuestionAndAnswer);
                return Ok(JsonQuestionAndAnswer);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddQuestionAndAnswer(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            try
            {
                QuestionAndAnswer.IDPersonel = AuUser.ReturnIDUser(QuestionAndAnswer.IDLogUser);
                QuestionAndAnswer.IDLogUser = null;
                bool ret = BisQuestionAndAnswer.AddQuestionAndAnswer(QuestionAndAnswer);
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
        public IHttpActionResult UpdateQuestionAndAnswer(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            try
            {
                QuestionAndAnswer.IDPersonel = AuUser.ReturnIDUser(QuestionAndAnswer.IDLogUser);
                QuestionAndAnswer.IDLogUser = null;
                bool ret = BisQuestionAndAnswer.UpdateQuestionAndAnswer(QuestionAndAnswer);
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
        public IHttpActionResult AcceptByManager(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            try
            {
                bool ret = BisQuestionAndAnswer.AcceptByManager(QuestionAndAnswer);
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
        public IHttpActionResult DenyByManager(ViewModel.tblQuestionAndAnswer QuestionAndAnswer)
        {
            try
            {
                bool ret = BisQuestionAndAnswer.DenyByManager(QuestionAndAnswer);
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
        public IHttpActionResult DeleteQuestionAndAnswer(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblQuestionAndAnswer DelQuestionAndAnswer = JsonObject.ToObject<ViewModel.tblQuestionAndAnswer>();
                bool ret = BisQuestionAndAnswer.DeleteQuestionAndAnswer(DelQuestionAndAnswer);
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