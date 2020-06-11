using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class OptionCustomerController : ApiController
    {
        Bis.OptionCustomerMethod BisOptionCustomer = new Bis.OptionCustomerMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetOptionCustomer(ViewModel.tblOptionCustomer OptionCustomer)
        {
            try
            {
                JArray JsonQuestionCustomer = BisOptionCustomer.GetOptionCustomer(OptionCustomer);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }



        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddOptionCustomer(ViewModel.tblOptionCustomer OptionCustomer)
        {
            try
            {
                bool ret = BisOptionCustomer.AddOptionCustomer(OptionCustomer);
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
        public IHttpActionResult UpdateOptionCustomer(ViewModel.tblOptionCustomer OptionCustomer)
        {
            try
            {
                bool ret = BisOptionCustomer.UpdateOptionCustomer(OptionCustomer);
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
        public IHttpActionResult ChangeSortInOptionCustomer(ViewModel.tblOptionCustomer OptionCustomer)
        {
            try
            {
                bool ret = BisOptionCustomer.ChangeSortInOptionCustomer(OptionCustomer);
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
        public IHttpActionResult DeleteOptionCustomer(ViewModel.tblOptionCustomer OptionCustomer)
        {
            try
            {
                bool ret = BisOptionCustomer.DeleteOptionCustomer(OptionCustomer);
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
