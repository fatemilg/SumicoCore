using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class FroalaUploaderController : ApiController
    {
        //--------------------Images-------------------------
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UploadImage()
        {
            try
            {
                var File = HttpContext.Current.Request.Files[0];
                Classes.FileTypes ft = new Classes.FileTypes();
                string FileType = File.FileName.Substring(File.FileName.LastIndexOf("."));
                int FileSize = File.ContentLength; //byte
                string FileUrl = @"File/FroalaImages/" + File.FileName;
                if (FileSize < 2 * 1024 * 1024 * 2 && ft.imgType().Contains(FileType.ToLower()))
                {
                    File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    JObject JsonResult = JObject.Parse("{'link' : ''}");
                    JsonResult["link"] = Request.RequestUri.Scheme + @"://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port + "/" + FileUrl;
                    return Ok(JsonResult);
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
        public IHttpActionResult DeleteImage(object obj)
        {
            try
            {
                string Path = HttpContext.Current.Request.Params["src"];
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + Path);
                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult LoadImageManagerList()
        {
            try
            {
                string[] Files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"File/FroalaImages/")
                                     .Select(Path.GetFileName)
                                     .ToArray();
                JArray JsonResult = new JArray();
                JObject JsonItem = new JObject();
                foreach (string FileName in Files)
                {
                    string path =  @"/File/FroalaImages/" + FileName;
                    JsonItem = JObject.Parse("{'url' : '" + path + "','thumb':'" + path + "','tag':'" + FileName + "'}");
                    JsonResult.Add(JsonItem);
                }


                return Ok(JsonResult);

            }
            catch
            {
                return NotFound();
            }
        }



        //--------------------Videos-------------------------
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UploadVideo()
        {
            try
            {
                var File = HttpContext.Current.Request.Files[0];
                Classes.FileTypes ft = new Classes.FileTypes();
                string FileType = File.FileName.Substring(File.FileName.LastIndexOf("."));
                int FileSize = File.ContentLength; //byte
                string FileUrl = @"/File/FroalaVideos/" + File.FileName;
                if (FileSize < 500 * 1024 * 1024  && ft.videoType().Contains(FileType.ToLower()))
                {
                    File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    JObject JsonResult = JObject.Parse("{'link' : ''}");
                    JsonResult["link"] = Request.RequestUri.Scheme + @"://" + Request.RequestUri.Host + ":" + Request.RequestUri.Port + "/" + FileUrl;
                    return Ok(JsonResult);
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
        public IHttpActionResult DeleteVideo(object obj)
        {
            try
            {
                string Path = HttpContext.Current.Request.Params["src"];
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + Path);
                return Ok();

            }
            catch
            {
                return NotFound();
            }
        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult LoadVideoManagerList()
        {
            try
            {
                string[] Files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"File/FroalaVideos/")
                                     .Select(Path.GetFileName)
                                     .ToArray();
                JArray JsonResult = new JArray();
                JObject JsonItem = new JObject();
                foreach (string FileName in Files)
                {
                    string path =  @"/File/FroalaVideos/" + FileName;
                    JsonItem = JObject.Parse("{'url' : '" + path + "','thumb':'" + path + "','tag':'" + FileName + "'}");
                    JsonResult.Add(JsonItem);
                }


                return Ok(JsonResult);

            }
            catch
            {
                return NotFound();
            }
        }
    }
}