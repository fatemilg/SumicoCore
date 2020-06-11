using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCMCore.Admin
{
    /// <summary>
    /// Summary description for getServerDate
    /// </summary>
    public class getServerDate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(DateTime.Now.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}