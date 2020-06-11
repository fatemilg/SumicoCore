using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;
using AjaxControlToolkit;

namespace SCMCore.Admin.Handler
{
    /// <summary>
    /// Summary description for HandlerAttachSite
    /// </summary>
    public class HandlerAttachSite : IHttpHandler
    {

        Bis.AttachSiteMethod BisAttachSiteMethod = new Bis.AttachSiteMethod();

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                try
                {
                    HttpPostedFile file = files[0];
                    if (file.ContentLength > 1000 * 1024 * 1024)
                    {
                        context.Response.Write(" سایز فایل باید کمتر از 50 مگابایت باشد  !");

                    }
                    else
                    {
                        string FilePath = context.Request.QueryString["FilePath"].ToString();
                        string Title = context.Request.QueryString["Title"].ToString();
                        string Description = context.Request.QueryString["Description"].ToString();
                        string UploadFileName = context.Request.QueryString["UploadFileName"].ToString();
                        string Order = context.Request.QueryString["Order"].ToString();
                        string IDRet = context.Request.QueryString["IDRet"].ToString();
                        string IdUser = context.Request.QueryString["IdUser"].ToString();
                        string IDAttachInterfaceCategory = context.Request.QueryString["IDAttachInterfaceCategory"].ToString();
                        string IDAttachSite = context.Request.QueryString["IDAttachSite"].ToString();

                        string FileType = Path.GetExtension(file.FileName).ToLower();
                        string FileName = IDAttachSite.ToString() + FileType;
                        ViewModel.tblAttachSite AddAttachSite = new ViewModel.tblAttachSite();
                        AddAttachSite.IDAttachSite = IDAttachSite.StringToGuid();
                        AddAttachSite.IDRet = IDRet.StringToGuid();
                        AddAttachSite.IDUser = IdUser.StringToGuid();
                        AddAttachSite.Name_Fa = Title;
                        AddAttachSite.Description_Fa = Description;
                        AddAttachSite.Url = FilePath + FileName;
                        AddAttachSite.FileType = FileType;
                        AddAttachSite.FileName = UploadFileName.Replace(FileType, "");
                        AddAttachSite.Order = Order.StringToInt();
                        AddAttachSite.IDAttachInterfaceCategory = IDAttachInterfaceCategory.StringToGuid();
                        AddAttachSite.Status = 1;
                        bool ret = BisAttachSiteMethod.AddAttachSite(AddAttachSite);
                        if (ret)
                        {
                            FilePath = context.Server.MapPath(@"\" + FilePath);
                            file.SaveAs(FilePath + FileName);
                            context.Response.Write("فایل مورد نظر آپلود شد!");
                        }
                        else
                        {
                            context.Response.Write("خطا در آپلود!");

                        }


                    }
                }
                catch
                {
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
    }
}