using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class ProductCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ProductCategoryMethod BisProductCategory = new Bis.ProductCategoryMethod();

        // GET api/menu
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillProductCategoryFromParentToMaster(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblProductCategory ProductCategorySearch = new ViewModel.tblProductCategory();
                ProductCategorySearch.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonContentCategory = BisProductCategory.GetProductCategoryJsonDataFromParentToMaster(ProductCategorySearch);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillProductCategory(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search SearchProductCategory = new ViewModel.Search();
                SearchProductCategory.Filter = " AND tblUser.IDX = " + JsonObject["IDXSupplier"].ToString().StringToInt().ToString() + " AND  ISNULL(ParentCategory.IDX,0)=" + JsonObject["IDXParentCategory"].ToString();
                SearchProductCategory.JsonResult = " FOR JSON PATH ";
                JArray JsonContentCategory = BisProductCategory.GetProductCategoryJsonDataSimple(SearchProductCategory);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillProductCategoryfromParentToMasterForSite(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblProductCategory SearchProductCategory = new ViewModel.tblProductCategory();

                SearchProductCategory.IDXSupplier = JsonObject["IDXSupplier"].ToString().StringToInt();
                SearchProductCategory.IDXProductCategory = JsonObject["IDXParentCategory"].ToString().StringToInt();
                JArray JsonContentCategory = BisProductCategory.GetJsonDataFromParentToChildForSite(SearchProductCategory);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillProductCategoryWithMasterProductForSOL(ViewModel.tblProductCategory obj)
        {
            try
            {
                JArray JsonContentCategory = BisProductCategory.GetDataWithMasterProductForSOL(obj);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetProductCategoryFromChildToParentForSiteMapMenu(ViewModel.tblProductCategory obj)
        {
            try
            {
                JArray JsonContentCategory = BisProductCategory.GetProductCategoryFromChildToParentForSiteMapMenu(obj);
                return Ok(JsonContentCategory);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
