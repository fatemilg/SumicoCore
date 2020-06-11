using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class TrainingCourseUserController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseUserMethod BisTrainingCourseUser = new Bis.TrainingCourseUserMethod();

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseUser()
        {
            try
            {
                ViewModel.Search TrainingCourseUserSearch = new ViewModel.Search();
                TrainingCourseUserSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseUser = BisTrainingCourseUser.GetTrainingCourseUserJsonData(TrainingCourseUserSearch);
                return Ok(JsonTrainingCourseUser);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseUser_ByIDTrainingCourse(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search TrainingCourseUserSearch = new ViewModel.Search();
                TrainingCourseUserSearch.Filter = " AND tblTrainingCourseUser.IDTrainingCourse = '" + JsonObject["IDTrainingCourse"].ToString() + "'";
                TrainingCourseUserSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseUser = BisTrainingCourseUser.GetTrainingCourseUserJsonData(TrainingCourseUserSearch);
                return Ok(JsonTrainingCourseUser);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourseUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseUser NewTrainingCourseUser = JsonObject.ToObject<ViewModel.tblTrainingCourseUser>();
                bool ret = BisTrainingCourseUser.AddTrainingCourseUser(NewTrainingCourseUser);
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
        public IHttpActionResult UpdateTrainingCourseUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseUser UpdateTrainingCourseUser = JsonObject.ToObject<ViewModel.tblTrainingCourseUser>();
                bool ret = BisTrainingCourseUser.UpdateTrainingCourseUser(UpdateTrainingCourseUser);
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
        public IHttpActionResult DeleteTrainingCourseUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseUser DelTrainingCourseUser = JsonObject.ToObject<ViewModel.tblTrainingCourseUser>();
                bool ret = BisTrainingCourseUser.DeleteTrainingCourseUser(DelTrainingCourseUser);
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



