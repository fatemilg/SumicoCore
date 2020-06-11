using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class QuestionCustomerController : ApiController
    {

        Bis.QuestionCustomerMethod BisQuestionCustomer = new Bis.QuestionCustomerMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQuestionCustomer()
        {
            try
            {
                ViewModel.tblQuestionCustomer QuestionCustomer = new ViewModel.tblQuestionCustomer();
                JArray JsonQuestionCustomer = BisQuestionCustomer.GetQuestionCustomer(QuestionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQuestionCustomerForSignUp()
        {
            try
            {
                ViewModel.tblQuestionCustomer QuestionCustomer = new ViewModel.tblQuestionCustomer();
                JArray JsonQuestionCustomer = BisQuestionCustomer.GetQuestionCustomerForSignUp(QuestionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQuestionCustomerForMaterialPageFirstStep(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                JArray JsonQuestionCustomer = BisQuestionCustomer.GetQuestionCustomerForMaterialPageFirstStep(QuestionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQuestionCustomerForMaterialPageOtherStep(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                JArray JsonQuestionCustomer = BisQuestionCustomer.GetQuestionCustomerForMaterialPageOtherStep(QuestionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult CheckSortSequently()
        {
            try
            {
                ViewModel.tblQuestionCustomer QuestionCustomer = new ViewModel.tblQuestionCustomer();
                JArray JsonQuestionCustomer = BisQuestionCustomer.CheckSortSequently(QuestionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddQuestionCustomer(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.AddQuestionCustomer(QuestionCustomer);
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
        public IHttpActionResult UpdateQuestionCustomer(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateQuestionCustomer(QuestionCustomer);
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
        public IHttpActionResult UpdateParentQuestionCustomer(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateParentQuestionCustomer(QuestionCustomer);
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
        public IHttpActionResult UpdateQuestionCustomerAsRoot(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateQuestionCustomerAsRoot(QuestionCustomer);
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
        public IHttpActionResult UpdateUseInCombination(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateUseInCombination(QuestionCustomer);
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
        public IHttpActionResult UpdateUseInSignUp(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateUseInSignUp(QuestionCustomer);
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
        public IHttpActionResult UpdateUseInMaterialDetail(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.UpdateUseInMaterialDetail(QuestionCustomer);
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
        public IHttpActionResult ChangeSortInQuestionCustomer(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.ChangeSortInQuestionCustomer(QuestionCustomer);
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
        public IHttpActionResult DeleteQuestionCustomer(ViewModel.tblQuestionCustomer QuestionCustomer)
        {
            try
            {
                bool ret = BisQuestionCustomer.DeleteQuestionCustomer(QuestionCustomer);
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
