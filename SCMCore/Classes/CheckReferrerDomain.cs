using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SCMCore.Classes
{
    public class CheckReferrerDomain : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {

                Projects project = new Projects();
                Uri referrer = HttpContext.Current.Request.UrlReferrer;
                string ClientDomain;
                if (referrer != null)
                {
                    if (referrer.ToString().Contains("192.168.44.109") || referrer.ToString().Contains("192.168.10.15")) //192.168.10.15 : Server *****  192.168.44.108 : Nedaei
                    {
                        ClientDomain = "localhost";
                    }
                    else
                    {
                        ClientDomain = referrer.Authority.Split(':')[0].Split('.').Count() == 3 ?
                                      referrer.Authority.Split(':')[0].Split('.')[1] :
                                      referrer.Authority.Split(':')[0].Split('.')[0]; //name domain ra joda mikonad. example: farbin.ir => farbin va text.farbin.ir => farbin

                    }

                    //string serverDomain = actionContext.Request.RequestUri.Authority; //scmcenter.ir
                    if (ClientDomain == "scmcenter")
                    {
                        ClientDomain = actionContext.Request.Headers.GetCookies().FirstOrDefault()["DomainName"].Value;
                    }
                    if (project.ReturnClientName(ClientDomain) == "")
                    {
                        var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "You can not access this method " + ClientDomain);
                        actionContext.Response = response;
                    }
                    else
                    {
                        HttpContext.Current.Items["DomainName"] = ClientDomain;
                    }
                }
            }
            catch (Exception ex)
            {
                var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "You can not access this method " + ex.Message);
                actionContext.Response = response;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }

    }
}