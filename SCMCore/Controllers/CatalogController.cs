using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class CatalogController : ApiController
    {
        Bis.CatalogMethod BisCatalog = new Bis.CatalogMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillCatalog()
        {
            try
            {
                ViewModel.Search SearchLegalUser = new ViewModel.Search();
                SearchLegalUser.Order = " order by tblCatalog.IDRet, tblCatalog.[Sort] Asc";
                SearchLegalUser.JsonResult = " FOR JSON path";
                JArray JsonCatalog = BisCatalog.GetJsonCatalogData(SearchLegalUser);
                foreach (JObject JItem in JsonCatalog)
                {
                    JItem.Add(new JProperty("SizeOfPDF", SizeOfFile(JItem["PDFUrl"].ToString())));
                }
                return Ok(JsonCatalog);
            }
            catch
            {
                return NotFound();
            }
        }

        protected string SizeOfFile(string path)
        {
            try
            {
                FileInfo file = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("/" + path));
                return (Math.Round(float.Parse(file.Length.ToString()) / (1024 * 1024),2)).ToString();
            }
            catch
            {
                return "0";
            }
        }
    }
}