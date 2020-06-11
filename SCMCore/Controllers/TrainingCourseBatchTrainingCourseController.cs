using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class TrainingCourseBatchTrainingCourseController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseBatchTrainingCourseMethod BisTrainingCourseBatchTrainingCourse = new Bis.TrainingCourseBatchTrainingCourseMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDataByIDTrainingCourseBatchByIDTrainingCourseBatch(ViewModel.tblTrainingCourseBatchTrainingCourse TrainingCourseBatchTrainingCourseSearch)
        {
            try
            {
                JArray JsonTrainingCourseBatchTrainingCourse = BisTrainingCourseBatchTrainingCourse.GetDataByIDTrainingCourseBatchByIDTrainingCourseBatch(TrainingCourseBatchTrainingCourseSearch);
                return Ok(JsonTrainingCourseBatchTrainingCourse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseBatchTainingCourseByIDTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse TrainingCourseBatchTrainingCourseSearch)
        {
            try
            {
                JArray JsonTrainingCourseBatchTrainingCourse = BisTrainingCourseBatchTrainingCourse.GetTrainingCourseBatchTainingCourseByIDTrainingCourse(TrainingCourseBatchTrainingCourseSearch);
                return Ok(JsonTrainingCourseBatchTrainingCourse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatchTrainingCourse.AddTrainingCourseBatchTrainingCourse(obj);
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
        public IHttpActionResult ToggleSelectTrainingCourseBatch(ViewModel.tblTrainingCourseBatchTrainingCourse obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatchTrainingCourse.ToggleSelectTrainingCourseBatch(obj);
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
        public IHttpActionResult UpdateTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatchTrainingCourse.UpdateTrainingCourseBatchTrainingCourse(obj);
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
        public IHttpActionResult DeleteTrainingCourseBatchTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatchTrainingCourse.DeleteTrainingCourseBatchTrainingCourse(obj);
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
        public IHttpActionResult ChangeSortTrainingCourse(ViewModel.tblTrainingCourseBatchTrainingCourse obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatchTrainingCourse.ChangeSortTrainingCourse(obj);
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



