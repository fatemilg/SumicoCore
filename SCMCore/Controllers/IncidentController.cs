using SCMCore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using System.IO;
using System.Drawing;

namespace SCMCore.Controllers
{
    public class IncidentController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.IncidentMethod BisIncident = new Bis.IncidentMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIncidentByIDIncident(ViewModel.tblIncident IncidentSearch)
        {
            try
            {
                JArray JsonIncident = BisIncident.GetIncidentJsonData_ByIDIncident(IncidentSearch);
                return Ok(JsonIncident);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIncidentAll(ViewModel.tblIncident IncidentSearch)
        {
            try
            {
                JArray JsonIncident = BisIncident.GetIncidentJsonData(IncidentSearch);
                return Ok(JsonIncident);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetIncidentData_ByIDX(ViewModel.tblIncident IncidentSearch)
        {
            try
            {
                JArray JsonIncident = BisIncident.GetIncidentJsonData_ByIDX(IncidentSearch);
                return Ok(JsonIncident);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddIncident(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblIncident NewIncident = JsonObject.ToObject<ViewModel.tblIncident>();
                byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image imageIncident = Image.FromStream(ms);
                FileTypes ft = new FileTypes();
                string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);
                if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                {

                    string FileUrl = @"Picture\Incident\" + Guid.NewGuid() + FileType;

                    NewIncident.PicUrl = FileUrl;
                    bool retAdd = BisIncident.AddIncident(NewIncident);
                    if (retAdd)
                    {
                        try
                        {
                            imageIncident.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                            return Ok(retAdd);
                        }
                        catch (Exception)
                        {
                            ViewModel.tblIncident delete = new ViewModel.tblIncident();
                            delete.IDIncident = NewIncident.IDIncident;
                            bool retDelete = BisIncident.DeleteIncident(delete);
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
        public IHttpActionResult UpdateIncident(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblIncident UpdateIncident = JsonObject.ToObject<ViewModel.tblIncident>();
                string FileUrl = "";
                ViewModel.tblIncident IncidentSearch = new ViewModel.tblIncident();
                IncidentSearch.IDIncident = UpdateIncident.IDIncident;
                JArray JsonIncident = BisIncident.GetIncidentJsonData_ByIDIncident(IncidentSearch);
                if (UpdateIncident.PicUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonIncident[0]["PicUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonIncident[0]["PicUrl"].ToString());
                }

                if (JsonObject["PicFile"].ToString() != "{}")
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageIncident = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        if (imageBytes.Length > 0)
                        {
                            FileUrl = @"Picture\Incident\" + Guid.NewGuid() + FileType;
                            UpdateIncident.PicUrl = FileUrl;
                            imageIncident.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        }
                    }
                }


                bool retUpdate = BisIncident.UpdateIncident(UpdateIncident);
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
        public IHttpActionResult DeleteIncident(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblIncident DelIncident = JsonObject.ToObject<ViewModel.tblIncident>();
                bool ret = BisIncident.DeleteIncident(DelIncident);
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



