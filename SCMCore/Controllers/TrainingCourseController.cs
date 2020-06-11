using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class TrainingCourseController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseMethod BisTrainingCourse = new Bis.TrainingCourseMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourse()
        {
            try
            {
                ViewModel.Search TrainingCourseSearch = new ViewModel.Search();
                TrainingCourseSearch.Order = " ORDER BY tblTrainingCourse.EndDate Desc ";
                TrainingCourseSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourse = BisTrainingCourse.GetTrainingCourseJsonData(TrainingCourseSearch);
                return Ok(JsonTrainingCourse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseDataForSiteByIDX(ViewModel.tblTrainingCourse TrainingCourseSearch)
        {
            try
            {
                JArray JsonTrainingCourse = BisTrainingCourse.GetDataForSiteByIDX(TrainingCourseSearch);
                return Ok(JsonTrainingCourse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetDataForSiteByIDXTrainingCourseCategory(ViewModel.tblTrainingCourseCategory TrainingCourseCategory)
        {
            try
            {
                JArray JsonTrainingCourse = BisTrainingCourse.GetDataForSiteByIDXTrainingCourseCategory(TrainingCourseCategory);
                return Ok(JsonTrainingCourse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourse(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourse NewTrainingCourse = JsonObject.ToObject<ViewModel.tblTrainingCourse>();
                byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image imageTrainingCourse = Image.FromStream(ms);
                FileTypes ft = new FileTypes();
                string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);
                if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                {

                    string FileUrl = @"Picture\TrainingCourse\" + NewTrainingCourse.IDTrainingCourse + FileType;

                    NewTrainingCourse.PicUrl = FileUrl;
                    bool retAdd = BisTrainingCourse.AddTrainingCourse(NewTrainingCourse);
                    if (retAdd)
                    {
                        try
                        {
                            imageTrainingCourse.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                            return Ok(retAdd);
                        }
                        catch (Exception)
                        {
                            ViewModel.tblTrainingCourse delete = new ViewModel.tblTrainingCourse();
                            delete.IDTrainingCourse = NewTrainingCourse.IDTrainingCourse;
                            bool retDelete = BisTrainingCourse.DeleteTrainingCourse(delete);
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
        public IHttpActionResult UpdateTrainingCourse(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourse UpdateTrainingCourse = JsonObject.ToObject<ViewModel.tblTrainingCourse>();
                string FileUrl = "";
                ViewModel.Search TrainingCourseSearch = new ViewModel.Search();
                TrainingCourseSearch.Filter = " AND tblTrainingCourse.IDTrainingCourse ='" + UpdateTrainingCourse.IDTrainingCourse + "'";
                TrainingCourseSearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourse = BisTrainingCourse.GetTrainingCourseJsonData(TrainingCourseSearch);
                if (UpdateTrainingCourse.PicUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourse[0]["PicUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourse[0]["PicUrl"].ToString());
                }

                if (JsonObject["PicFile"].ToString() != "{}")
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageTrainingCourse = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        if (imageBytes.Length > 0)
                        {
                            FileUrl = @"Picture\TrainingCourse\" + Guid.NewGuid() + FileType;
                            UpdateTrainingCourse.PicUrl = FileUrl;
                            imageTrainingCourse.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        }
                    }
                }


                bool retUpdate = BisTrainingCourse.UpdateTrainingCourse(UpdateTrainingCourse);
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
        public IHttpActionResult DeleteTrainingCourse(ViewModel.tblTrainingCourse DelTrainingCourse)
        {
            try
            {
                ViewModel.Search objSearch = new ViewModel.Search();
                objSearch.Filter = " AND tblTrainingCourse.IDTrainingCourse = '" + DelTrainingCourse.IDTrainingCourse + "'";
                objSearch.JsonResult = " For Json Path";
                JArray JsonTrainingCourse = BisTrainingCourse.GetTrainingCourseJsonData(objSearch);
                bool ret = BisTrainingCourse.DeleteTrainingCourse(DelTrainingCourse);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourse[0]["PicUrl"]);
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



