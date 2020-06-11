using SCMCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SCMCore.Controllers
{
    public class SiteSettingController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSiteSetting()
        {
            try
            {
                string DomainName = "";
                SiteSetting objSiteSetting = new SiteSetting();
                if (HttpContext.Current.Items["DomainName"] != null)
                {
                    DomainName = HttpContext.Current.Items["DomainName"].ToString();
                    
                    if (DomainName == "localhost")
                    {
                        objSiteSetting.DefaultLanguage = "En";
                        objSiteSetting.EnLanguage = "false";
                        objSiteSetting.FaLanguage = "false";
                        objSiteSetting.ProductCategoryTreeStatus = "Show";
                        objSiteSetting.VersionNo = "1.5";
                    }
                    else if (DomainName == "farbin")
                    {
                        objSiteSetting.DefaultLanguage = "En";
                        objSiteSetting.EnLanguage = "true";
                        objSiteSetting.FaLanguage = "false";
                        objSiteSetting.ProductCategoryTreeStatus = "Show";
                        objSiteSetting.VersionNo = "1.5";
                    }
                    
                }
                return Ok(objSiteSetting);
            }
            catch
            {
                return NotFound();
            }

        }

    }
}