using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class FormOptionController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.FormOptionMethod BisFormOption = new Bis.FormOptionMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetFormOptionDataByIDFormQuestion(ViewModel.tblFormOption objFormOption)
        {
            try
            {
                JArray JsonFormOption = BisFormOption.GetFormOptionDataByIDFormQuestion(objFormOption);
                return Ok(JsonFormOption);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddFormOption(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblFormOption NewFormOption = JsonObject.ToObject<ViewModel.tblFormOption>();
                if (JsonObject["PicFile"] != null)
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageFormOption = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);
                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {

                        string FileUrl = @"Picture\FormOption\" + NewFormOption.IDFormOption + FileType;

                        NewFormOption.PicUrl = FileUrl;
                        bool retAdd = BisFormOption.AddFormOption(NewFormOption);
                        if (retAdd)
                        {
                            try
                            {
                                imageFormOption.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                                return Ok(retAdd);
                            }
                            catch (Exception)
                            {
                                ViewModel.tblFormOption delete = new ViewModel.tblFormOption();
                                delete.IDFormOption = NewFormOption.IDFormOption;
                                bool retDelete = BisFormOption.DeleteFormOption(delete);
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
                    NewFormOption.PicUrl = "";
                    bool retAdd = BisFormOption.AddFormOption(NewFormOption);
                    if (retAdd)
                    {
                        try
                        {
                            return Ok(retAdd);
                        }
                        catch (Exception)
                        {
                            return NotFound();
                        }
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
        public IHttpActionResult UpdateFormOption(object obj)
        {

            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblFormOption UpdateFormOption = JsonObject.ToObject<ViewModel.tblFormOption>();
                string FileUrl = "";
                ViewModel.tblFormOption FormOptionSearch = new ViewModel.tblFormOption();
                FormOptionSearch.IDFormOption = UpdateFormOption.IDFormOption;
                JArray JsonFormOption = BisFormOption.GetDataByIDFormOption(FormOptionSearch);
                if (UpdateFormOption.PicUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonFormOption[0]["PicUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonFormOption[0]["PicUrl"].ToString());
                }

                if (JsonObject["PicFile"].ToString() != "{}")
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageFormOption = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        if (imageBytes.Length > 0)
                        {
                            FileUrl = @"Picture\FormOption\" + Guid.NewGuid() + FileType;
                            UpdateFormOption.PicUrl = FileUrl;
                            imageFormOption.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        }
                    }
                }


                bool retUpdate = BisFormOption.UpdateFormOption(UpdateFormOption);
                if (retUpdate)
                {
                    return Ok(retUpdate);
                }
                else
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteFormOption(ViewModel.tblFormOption DelFormOption)
        {
            try
            {
                JArray JsonFormOption = BisFormOption.GetDataByIDFormOption(DelFormOption);
                bool ret = BisFormOption.DeleteFormOption(DelFormOption);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonFormOption[0]["PicUrl"]);
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
        public IHttpActionResult ChangeSortOptions(ViewModel.tblFormOption obj)
        {
            try
            {
                bool ret = BisFormOption.ChangeSortOptions(obj);
                if (ret)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}



