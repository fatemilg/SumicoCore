using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class GalleryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.GalleryMethod BisGallery = new Bis.GalleryMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillGalleryByIDRet(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblGallery GetGallery = JsonObject.ToObject<ViewModel.tblGallery>();
                ViewModel.Search GallerySearch = new ViewModel.Search();
                GallerySearch.Filter = " AND tblGallery.IDRet ='" + GetGallery.IDRet + "'";
                GallerySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonGallery = BisGallery.GetGalleryJsonData(GallerySearch);
                return Ok(JsonGallery);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillGalleryByIDGalleryCategory(ViewModel.tblGallery obj)
        {
            try
            {
                ViewModel.Search GallerySearch = new ViewModel.Search();
                GallerySearch.Filter = " AND tblGallery.IDGalleryCategory ='" + obj.IDGalleryCategory + "' AND tblGallery.IDRet = '" + obj.IDRet + "'";
                GallerySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonGallery = BisGallery.GetGalleryJsonData(GallerySearch);
                return Ok(JsonGallery);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddGallery(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());

                for (int i = 0; i < JsonObject["ObjFiles"].Count(); i++)
                {
                    ViewModel.tblGallery NewGallery = JsonObject["ObjGallery"][i.ToString()].ToObject<ViewModel.tblGallery>();
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["ObjFiles"][i.ToString()].ToString().Split(',')[1]);// dataye file ersali
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageGallery = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["ObjFiles"][i.ToString()].ToString().Split(',')[0]); // sakhtare file ersali

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        string FileUrl = @"Picture\Gallery\" + NewGallery.IDGallery + FileType;
                        NewGallery.Url = FileUrl;
                        bool retAdd = BisGallery.AddGallery(NewGallery);
                        if (retAdd)
                        {
                            try
                            {
                                imageGallery.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);

                            }
                            catch (Exception)
                            {
                                ViewModel.tblGallery delete = new ViewModel.tblGallery();
                                delete.IDGallery = NewGallery.IDGallery;
                                bool retDelete = BisGallery.DeleteGallery(delete);
                                return NotFound();
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound();
            }


        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddFileToGallery(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());

                for (int i = 0; i < JsonObject["ObjFiles"].Count(); i++)
                {
                    ViewModel.tblGallery NewGallery = JsonObject["ObjGallery"][i.ToString()].ToObject<ViewModel.tblGallery>();
                    byte[] FileBytes = Convert.FromBase64String(JsonObject["ObjFiles"][i.ToString()].ToString().Split(',')[1]);// dataye file ersali
                    MemoryStream ms = new MemoryStream(FileBytes, 0,
                      FileBytes.Length);

                    ms.Write(FileBytes, 0, FileBytes.Length);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindFileTypeInString(JsonObject["ObjFiles"][i.ToString()].ToString().Split(',')[0]); // sakhtare file ersali

                    if (FileBytes.Length < 50 * 1024 * 1024)
                    {
                        string FileUrl = @"Picture\Gallery\" + NewGallery.IDGallery + FileType;
                        NewGallery.Url = FileUrl;
                        bool retAdd = BisGallery.AddGallery(NewGallery);
                        if (retAdd)
                        {
                            try
                            {
                                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + FileUrl, FileBytes);
                            }
                            catch (Exception ex)
                            {
                                ViewModel.tblGallery delete = new ViewModel.tblGallery();
                                delete.IDGallery = NewGallery.IDGallery;
                                bool retDelete = BisGallery.DeleteGallery(delete);
                                return NotFound();
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound();
            }


        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteGallery(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search GallerySearch = new ViewModel.Search();
                GallerySearch.Filter = " AND tblGallery.IDGallery ='" + JsonObject["IDGallery"].ToString() + "'";
                GallerySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonGallery = BisGallery.GetGalleryJsonData(GallerySearch);


                ViewModel.tblGallery DelGallery = JsonObject.ToObject<ViewModel.tblGallery>();
                bool ret = BisGallery.DeleteGallery(DelGallery);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonGallery[0]["Url"]);
                    return Ok(ret);
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

    }
}