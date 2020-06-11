using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SCMCore.Admin.Handler
{
    /// <summary>
    /// Summary description for DeleteFile
    /// </summary>
    public class DeleteFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string FileName = context.Request.QueryString["FileName"].ToString();
                string FileNamePath = AppDomain.CurrentDomain.BaseDirectory + @"Admin\TempUploadFile\" + FileName;
                File.Delete(FileNamePath);
            }
            catch
            {
            }
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