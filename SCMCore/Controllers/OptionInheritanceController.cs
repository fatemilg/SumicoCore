using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;


namespace SCMCore.Controllers
{
    public class OptionInheritanceController : ApiController
    {
        Bis.OptionInheritanceMethod BisOptionInheritance = new Bis.OptionInheritanceMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetOptionInheritance(ViewModel.tblOptionInheritance OptionInheritance)
        {
            try
            {
                JArray JsonQuestionCustomer = BisOptionInheritance.GetOptionInheritance(OptionInheritance);
                return Ok(JsonQuestionCustomer);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult InitialOptionInheritance(ViewModel.tblOptionInheritance obj)
        {
            try
            {
                bool ret = BisOptionInheritance.InitialOptionInheritance(obj);
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
