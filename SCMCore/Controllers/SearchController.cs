using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class SearchController : ApiController
    {
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();
        Bis.ProductMethod BisProduct = new Bis.ProductMethod();
        Bis.ContentMethod BisContet = new Bis.ContentMethod();
        Bis.TrainingCourseMethod BisTrainingCourse = new Bis.TrainingCourseMethod();
        Bis.TrainingCourseUserMethod BisTrainingCourseUser = new Bis.TrainingCourseUserMethod();
        Bis.TrainingCourseBatchMethod BisTrainingCourseBatch = new Bis.TrainingCourseBatchMethod();
        Bis.RulePropertyProductMethod BisRulePropertyProduct = new Bis.RulePropertyProductMethod();
        Bis.IncidentMethod BisIncident = new Bis.IncidentMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchRuleDefineProduct(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JObject JsonTotalResult = new JObject();
                JArray JsonRuleDefine = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchProduct = new ViewModel.Search();
                        searchProduct.Filter = SearchText.FixFarsi();
                        JsonRuleDefine = BisRulePropertyProduct.GetFullJsonDataForSearch(searchProduct);
                    }
                    catch
                    { }
                    JsonTotalResult = JObject.Parse("{'RuleDefine': ''}");
                    JsonTotalResult["RuleDefine"] = JsonRuleDefine;
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonTotalResult);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JArray JsonContent = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchContent = new ViewModel.Search();
                        searchContent.Filter = SearchText.FixFarsi();
                        JsonContent = BisContet.GetFullJsonDataForSearch(searchContent);
                    }
                    catch
                    { return NotFound(); }
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonContent);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchTrainingCourse(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JArray JsonTrainingCourse = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchTrainingCourse = new ViewModel.Search();
                        searchTrainingCourse.Filter = SearchText.FixFarsi();
                        JsonTrainingCourse = BisTrainingCourse.GetFullJsonDataForSearch(searchTrainingCourse);
                    }
                    catch
                    { return NotFound(); }
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonTrainingCourse);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchTrainingCourseUser(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JArray JsonTrainingCourseUser = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchTrainingCourseUser = new ViewModel.Search();
                        searchTrainingCourseUser.Filter = SearchText.FixFarsi();
                        JsonTrainingCourseUser = BisTrainingCourseUser.GetFullJsonDataForSearch(searchTrainingCourseUser);
                    }
                    catch
                    { return NotFound(); }
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonTrainingCourseUser);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchTrainingCourseBatch(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JArray JsonTrainingCourseBatch = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchTrainingCourseBatch = new ViewModel.Search();
                        searchTrainingCourseBatch.Filter = SearchText.FixFarsi();
                        JsonTrainingCourseBatch = BisTrainingCourseBatch.GetFullJsonDataForSearch(searchTrainingCourseBatch);
                    }
                    catch
                    { return NotFound(); }
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonTrainingCourseBatch);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SearchIncindet(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JArray JsonIncident = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchIncident = new ViewModel.Search();
                        searchIncident.Filter = SearchText.FixFarsi();
                        JsonIncident = BisIncident.GetFullJsonDataForSearch(searchIncident);
                    }
                    catch
                    { return NotFound(); }
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonIncident);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillSearchForSumico(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                JObject JsonTotalResult = new JObject();
                JArray JsonRuleDefine = new JArray();
                string SearchText = JsonObject["SearchText"].ToString();
                if (SearchText.Length > 2)
                {
                    try
                    {
                        ViewModel.Search searchProduct = new ViewModel.Search();
                        searchProduct.Filter = SearchText.FixFarsi();
                        JsonRuleDefine = BisRulePropertyProduct.GetFullJsonDataForSearchForSumico(searchProduct);
                    }
                    catch
                    { }

                    JsonTotalResult = JObject.Parse("{'ProductCategory': '','MasterProduct': '','RuleDefine': ''}");
                    JsonTotalResult["RuleDefine"] = JsonRuleDefine;
                }
                else
                {
                    return NotFound();
                }
                return Ok(JsonTotalResult);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}