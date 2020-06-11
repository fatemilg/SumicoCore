using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class TrainingCourseContentController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseContentMethod BisTrainingCourseContent = new Bis.TrainingCourseContentMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseContent()
        {
            try
            {
                ViewModel.Search TrainingCourseContentSearch = new ViewModel.Search();
                TrainingCourseContentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseContent = BisTrainingCourseContent.GetTrainingCourseContentJsonData(TrainingCourseContentSearch);
                return Ok(JsonTrainingCourseContent);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseContent_ByIDTrainningCourse(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseContent GetTrainingCourseContent = JsonObject.ToObject<ViewModel.tblTrainingCourseContent>();
                JArray JsonTrainingCourseContent = BisTrainingCourseContent.GetTrainingCourseContentJsonData_ByIDTrainingCourse(GetTrainingCourseContent);
                return Ok(JsonTrainingCourseContent);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourseContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseContent NewTrainingCourseContent = JsonObject.ToObject<ViewModel.tblTrainingCourseContent>();
                bool ret = BisTrainingCourseContent.AddTrainingCourseContent(NewTrainingCourseContent);
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
        public IHttpActionResult SaveTrainingCourseContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseContent NewTrainingCourseContent = JsonObject.ToObject<ViewModel.tblTrainingCourseContent>();
                bool ret = BisTrainingCourseContent.SaveTrainingCourseContent(NewTrainingCourseContent);
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
        public IHttpActionResult UpdateTrainingCourseContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseContent UpdateTrainingCourseContent = JsonObject.ToObject<ViewModel.tblTrainingCourseContent>();
                bool ret = BisTrainingCourseContent.UpdateTrainingCourseContent(UpdateTrainingCourseContent);
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
        public IHttpActionResult DeleteTrainingCourseContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseContent DelTrainingCourseContent = JsonObject.ToObject<ViewModel.tblTrainingCourseContent>();
                bool ret = BisTrainingCourseContent.DeleteTrainingCourseContent(DelTrainingCourseContent);
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



