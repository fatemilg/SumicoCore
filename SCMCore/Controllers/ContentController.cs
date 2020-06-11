using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class ContentController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.ContentMethod BisContent = new Bis.ContentMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillContentByID(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search ContentSearch = new ViewModel.Search();
                ContentSearch.Filter = " AND tblContent.IDContent ='" + JsonObject["IDContent"].ToString() + "'";
                ContentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContent = BisContent.GetContentJsonData(ContentSearch);
                return Ok(JsonContent);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult FillContentByIDX(ViewModel.tblContent obj)
        {
            try
            {
                ViewModel.Search ContentSearch = new ViewModel.Search();
                ContentSearch.Filter = " AND tblContent.IDX ='" + obj.IDX.ToString() + "'";
                ContentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContent = BisContent.GetContentJsonData(ContentSearch);
                return Ok(JsonContent);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTopArticleJsonData()
        {
            try
            {

                ViewModel.Search ContentSearch = new ViewModel.Search();
                ContentSearch.Filter = "  AND tblContentCategoryType.Name_En ='Articles' AND tblContent.InTopCategoryView = 1";
                ContentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContent = BisContent.GetContentJsonDataByCategoryType(ContentSearch);
                return Ok(JsonContent);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContent NewContent = JsonObject["Content"].ToObject<ViewModel.tblContent>();
                NewContent.IDPersonel = AuUser.ReturnIDUser(JsonObject["Authorization"]["IDLogUser"].ToString().StringToGuid());
                if (NewContent.IDPersonel != null && NewContent.IDPersonel != Guid.Empty)
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageContent = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {

                        string FileUrl = @"Picture\Content\";

                        NewContent.PicUrl = FileUrl+ NewContent.IDContent + FileType;
                        bool retAdd = BisContent.AddContent(NewContent);
                        if (retAdd)
                        {
                            try
                            {
                                imageContent.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl + NewContent.IDContent + FileType);
                                imageContent.resizeImage(300, null).Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl + @"Small\" + NewContent.IDContent + FileType);

                                return Ok(retAdd);
                            }
                            catch (Exception ex)
                            {
                                ViewModel.tblContent cntdelete = new ViewModel.tblContent();
                                cntdelete.IDContent = NewContent.IDContent;
                                bool retDelete = BisContent.DeleteContent(cntdelete);
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
        public IHttpActionResult LikeContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContent ObjContent = JsonObject.ToObject<ViewModel.tblContent>();
                string LikeStatus = JsonObject["LikeStatus"].ToString();
                bool retLike = false;
                if (LikeStatus == "Like")
                {
                    retLike = BisContent.LikeContent(ObjContent);
                    if (retLike)
                    {
                        return Ok("Like");

                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    retLike = BisContent.UnLikeContent(ObjContent);
                    if (retLike)
                    {
                        return Ok("UnLike");

                    }
                    else
                    {
                        return NotFound();
                    }
                }


            }
            catch
            {
                return NotFound();
            }


        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblContent UpdateContent = JsonObject["Content"].ToObject<ViewModel.tblContent>();
                UpdateContent.IDPersonel = AuUser.ReturnIDUser(JsonObject["Authorization"]["IDLogUser"].ToString().StringToGuid());
                if (UpdateContent.IDPersonel != null && UpdateContent.IDPersonel != Guid.Empty)
                {
                    string FileUrl = "";

                    ViewModel.Search ContentSearch = new ViewModel.Search();
                    ContentSearch.Filter = " AND tblContent.IDContent ='" + UpdateContent.IDContent + "'";
                    ContentSearch.JsonResult = " FOR JSON PATH ";
                    JArray JsonContent = BisContent.GetContentJsonData(ContentSearch);
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"].ToString()) && JsonObject["PicFile"].ToString() != "{}")
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"].ToString());
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"].ToString().AddSmallUrl());
                        UpdateContent.PicUrl = "";
                    }

                    if (JsonObject["PicFile"].ToString() != "{}")
                    {
                        byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                        MemoryStream ms = new MemoryStream(imageBytes, 0,
                          imageBytes.Length);

                        ms.Write(imageBytes, 0, imageBytes.Length);
                        Image imageContent = Image.FromStream(ms);
                        FileTypes ft = new FileTypes();
                        string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                        if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                        {
                            if (imageBytes.Length > 0)
                            {
                                Guid FileGUIDName = Guid.NewGuid();
                                FileUrl = @"Picture\Content\";
                                UpdateContent.PicUrl = FileUrl+ FileGUIDName + FileType;
                                imageContent.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl + FileGUIDName + FileType);
                                imageContent.resizeImage(300, null).Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl + @"Small\" + FileGUIDName + FileType);

                            }
                        }
                    }


                    bool retAdd = BisContent.UpdateContent(UpdateContent);
                    if (retAdd)
                    {
                        return Ok(retAdd);
                    }
                    else
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        return NotFound();
                    }

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
        public IHttpActionResult DeleteContent(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.Search ContentSearch = new ViewModel.Search();
                ContentSearch.Filter = " AND tblContent.IDContent ='" + JsonObject["IDContent"].ToString() + "'";
                ContentSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonContent = BisContent.GetContentJsonData(ContentSearch);


                ViewModel.tblContent DelContent = JsonObject.ToObject<ViewModel.tblContent>();
                bool ret = BisContent.DeleteContent(DelContent);
                if (ret)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"]))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"]);
                    }
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"].ToString().AddSmallUrl()))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonContent[0]["PicUrl"].ToString().AddSmallUrl());
                    }
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

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ToggleActivation(ViewModel.tblContent obj)
        {
            try
            {
                bool ret = BisContent.ToggleActivation(obj);
                if (ret)
                {
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