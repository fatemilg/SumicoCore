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
    public class DictionaryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.DictionaryMethod BisDictionary = new Bis.DictionaryMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetAllDictionary()
        {
            try
            {
                ViewModel.Search DictionarySearch = new ViewModel.Search();
                DictionarySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonDictionary = BisDictionary.GetDictionaryJsonData(DictionarySearch);
                return Ok(JsonDictionary);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDictionaryByIDX(ViewModel.tblDictionary obj)
        {
            try
            {
                ViewModel.Search DictionarySearch = new ViewModel.Search();
                DictionarySearch.Filter = " AND tblDictionary.IDX = " + obj.IDX.ToString(); ;
                DictionarySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonDictionary = BisDictionary.GetDictionaryJsonData(DictionarySearch);
                return Ok(JsonDictionary);
            }
            catch
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddDictionary(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDictionary NewDictionary = JsonObject.ToObject<ViewModel.tblDictionary>();
                string FileUrl = "";
                if (NewDictionary.PicUrl != "" && NewDictionary.PicUrl != null)
                {

                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicUrl"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageDictionary = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicUrl"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        FileUrl = @"Picture\Dictionary\" + NewDictionary.IDDictionary + FileType;
                        imageDictionary.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                NewDictionary.PicUrl = FileUrl;
                bool ret = BisDictionary.AddDictionary(NewDictionary);
                if (ret)
                {

                    return Ok(ret);
                }

                else { return NotFound(); }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateDictionary(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDictionary UpdateDictionary = JsonObject.ToObject<ViewModel.tblDictionary>();
                string FileUrl = "";
                if (UpdateDictionary.PicUrl.Contains("data:image"))
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicUrl"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageDictionary = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicUrl"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        FileUrl = @"Picture\Dictionary\" + UpdateDictionary.IDDictionary + FileType;
                        UpdateDictionary.PicUrl = FileUrl;
                        imageDictionary.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    }
                    else { return NotFound(); }
                }
                bool ret = BisDictionary.UpdateDictionary(UpdateDictionary);
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
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteDictionary(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblDictionary DelDictionary = JsonObject.ToObject<ViewModel.tblDictionary>();
                bool ret = BisDictionary.DeleteDictionary(DelDictionary);
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

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ToggleActivation(ViewModel.tblDictionary obj)
        {
            try
            {
                bool ret = BisDictionary.ToggleActivation(obj);
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

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetOneRecordRandom()
        {
            try
            {
                ViewModel.tblDictionary objDictionary = new ViewModel.tblDictionary();
                JArray JsonDictionary = BisDictionary.GetOneRecordRandom(objDictionary);
                return Ok(JsonDictionary);

            }
            catch
            {
                return NotFound();
            }
        }
    }
}