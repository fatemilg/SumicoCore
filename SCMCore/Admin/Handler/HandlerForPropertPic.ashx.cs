using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SCMCore.Admin.Handler
{
    /// <summary>
    /// Summary description for HandlerForPropertPic
    /// </summary>
    public class HandlerForPropertPic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                try
                {
                    HttpPostedFile file = files[0];
                    if (file.ContentLength < 3 * 1024 * 1024)
                    {
                        string FileType = Path.GetExtension(file.FileName).ToLower();
                        string[] ValidTypes = { ".doc", ".docx", ".txt", ".pdf", ".rar", ".zip", ".jpg", ".png", ".gif", ".mp4" ,".vss"};
                        if (ValidTypes.Contains(FileType))
                        {
                            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Picture\Property";
                            string FileName = context.Request.QueryString["FileName"].ToString();
                            FilePath = FilePath + "\\" + FileName + FileType;
                            file.SaveAs(FilePath);
                            context.Response.Write("فایل مورد نظر آپلود شد!");
                        }
                        else
                        {
                            context.Response.Write("فرمت فایل انتخابی مناسب نیست!");
                        }
                    }
                    else
                    {
                        context.Response.Write("سایز فایل باید کم تر از 3 مگابایت باشد!");
                    }
                }
                catch (Exception ex)
                {
                    string[] ErrorLines = { DateTime.Now.ToString(), "On Uploading Files", ex.Message, "**********\n" };
                    WriteError(ErrorLines);
                    context.Response.Write("خطا!فایل مورد نظر آپلود نشد");
                }
            }

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        protected void WriteError(string[] ErrorLines)
        {
            string ErrorFilePath = AppDomain.CurrentDomain.BaseDirectory + @"..\Error\ErrorLog.txt";
            using (StreamWriter TW = File.AppendText(ErrorFilePath))
            {
                foreach (string str in ErrorLines)
                {
                    TW.WriteLine(str);
                }
            }
        }
    }
}