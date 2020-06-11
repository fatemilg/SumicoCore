using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class CombinationOptionsController : ApiController
    {
        Bis.CombinationOptionsMethod BisCombinationOptions = new Bis.CombinationOptionsMethod();
        AuthorizationUser AuUser = new AuthorizationUser();


        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCombinationOptionsByIDSeller(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                JArray JsonCombinationOptions = BisCombinationOptions.GetCombinationOptionsByIDSeller(obj);
                return Ok(JsonCombinationOptions);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCountCombinationOptions()
        {
            try
            {
                ViewModel.tblCombinationOptions obj = new ViewModel.tblCombinationOptions();
                JArray JsonCombinationOptions = BisCombinationOptions.GetCountCombinationOptions(obj);
                return Ok(JsonCombinationOptions);
            }
            catch
            {
                return NotFound();
            }

        }
    

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetRestOtItemsNotExistInRoot(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                JArray JsonCombinationOptions = BisCombinationOptions.GetRestOtItemsNotExistInRoot(obj);
                return Ok(JsonCombinationOptions);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetRestOtItemsNotExistForOtherLevel(ViewModel.tblCombinationOptions obj) 
        {
            try
            {
                JArray JsonCombinationOptions = BisCombinationOptions.GetRestOtItemsNotExistForOtherLevel(obj);
                return Ok(JsonCombinationOptions);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetRestOtItemsNotExistInRootByIDOptionInheritance(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                JArray JsonCombinationOptions = BisCombinationOptions.GetRestOtItemsNotExistInRootByIDOptionInheritance(obj);
                return Ok(JsonCombinationOptions);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddCombinationOptionsForRoot(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                bool ret = BisCombinationOptions.AddCombinationOptionsForRoot(obj);
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
        public IHttpActionResult AddCombinationOptionsForNextLevels(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                bool ret = BisCombinationOptions.AddCombinationOptionsForNextLevels(obj);
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
        public IHttpActionResult DeleteCombinationOptionsBYIDCombinationOptions(ViewModel.tblCombinationOptions obj)
        {
            try
            {
                bool ret = BisCombinationOptions.DeleteCombinationOptionsBYIDCombinationOptions(obj);
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
        public IHttpActionResult DeleteAllCombination()
        {
            try
            {
                ViewModel.tblCombinationOptions obj = new ViewModel.tblCombinationOptions();
                bool ret = BisCombinationOptions.DeleteAllCombination(obj);
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
