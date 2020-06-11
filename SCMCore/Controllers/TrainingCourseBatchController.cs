using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class TrainingCourseBatchController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseBatchMethod BisTrainingCourseBatch = new Bis.TrainingCourseBatchMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseBatch()
        {
            try
            {
                ViewModel.tblTrainingCourseBatch TrainingCourseBatchSearch = new ViewModel.tblTrainingCourseBatch();
                JArray JsonTrainingCourseBatch = BisTrainingCourseBatch.GetTrainingCourseBatchJsonData(TrainingCourseBatchSearch);
                return Ok(JsonTrainingCourseBatch);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDataForSiteWithTrainingCourse()
        {
            try
            {
                ViewModel.tblTrainingCourseBatch TrainingCourseBatchSearch = new ViewModel.tblTrainingCourseBatch();
                JArray JsonTrainingCourseBatch = BisTrainingCourseBatch.GetDataForSiteWithTrainingCourse(TrainingCourseBatchSearch);
                return Ok(JsonTrainingCourseBatch);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDataForSiteWithTrainingCourse_ByIDXTrainingCourseBatch(ViewModel.tblTrainingCourseBatch TrainingCourseBatchSearch)
        {
            try
            {
                JArray JsonTrainingCourseBatch = BisTrainingCourseBatch.GetDataForSiteWithTrainingCourse_ByIDXTrainingCourseBatch(TrainingCourseBatchSearch);
                return Ok(JsonTrainingCourseBatch);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourseBatch(ViewModel.tblTrainingCourseBatch obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatch.AddTrainingCourseBatch(obj);
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
        public IHttpActionResult UpdateTrainingCourseBatch(ViewModel.tblTrainingCourseBatch obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatch.UpdateTrainingCourseBatch(obj);
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
        public IHttpActionResult DeleteTrainingCourseBatch(ViewModel.tblTrainingCourseBatch obj)
        {
            try
            {
                bool ret = BisTrainingCourseBatch.DeleteTrainingCourseBatch(obj);
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



