using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class TrainingCourseCategoryController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.TrainingCourseCategoryMethod BisTrainingCourseCategory = new Bis.TrainingCourseCategoryMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseCategory()
        {
            try
            {
                ViewModel.Search TrainingCourseCategorySearch = new ViewModel.Search();
                TrainingCourseCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryJsonData(TrainingCourseCategorySearch);
                return Ok(JsonTrainingCourseCategory);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseCategoryByIDX(ViewModel.tblTrainingCourseCategory obj)
        {
            try
            {
                ViewModel.Search TrainingCourseCategorySearch = new ViewModel.Search();
                TrainingCourseCategorySearch.Filter = " AND tblTrainingCourseCategory.IDX =" + obj.IDX;
                TrainingCourseCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryJsonData(TrainingCourseCategorySearch);
                return Ok(JsonTrainingCourseCategory);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetTrainingCourseCategory_Tree()
        {
            try
            {
                ViewModel.tblTrainingCourseCategory TrainingCourseCategorySearch = new ViewModel.tblTrainingCourseCategory();
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryData_Tree(TrainingCourseCategorySearch);
                return Ok(JsonTrainingCourseCategory);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetNestedTrainingCourseCategory()
        {
            try
            {
                ViewModel.tblTrainingCourseCategory TrainingCourseCategorySearch = new ViewModel.tblTrainingCourseCategory();
                TrainingCourseCategorySearch.IDParent = Guid.Empty;
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryJsonNestedData(TrainingCourseCategorySearch);
                return Ok(JsonTrainingCourseCategory);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddTrainingCourseCategory(object obj)
        {
            try
            {
                string FileUrl, FileUrlPDF;
                FileTypes ft = new FileTypes();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseCategory NewTrainingCourseCategory = JsonObject.ToObject<ViewModel.tblTrainingCourseCategory>();

                //-----------------START Pic-----------------------
                byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image imageTrainingCourseCategory = Image.FromStream(ms);

                string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);
                if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                {
                    FileUrl = @"Picture\TrainingCourseCategory\" + NewTrainingCourseCategory.IDTrainingCourseCategory + FileType;
                    NewTrainingCourseCategory.PicUrl = FileUrl;

                }
                else
                {
                    return NotFound();
                }
                //-----------------EDN Pic-----------------------

                //-----------------START AttachPDF-----------------------
                byte[] FileBytes = Convert.FromBase64String(JsonObject["AttachPDFFile"].ToString().Split(',')[1]);// dataye file ersali
                MemoryStream msPDF = new MemoryStream(FileBytes, 0,
                  FileBytes.Length);

                msPDF.Write(FileBytes, 0, FileBytes.Length);
                string FileTypePDF = ft.FindFileTypeInString(JsonObject["AttachPDFFile"].ToString().Split(',')[0]); // sakhtare file ersali

                if (FileBytes.Length < 50 * 1024 * 1024)
                {
                    FileUrlPDF = @"Picture\TrainingCourseCategory\" + Guid.NewGuid() + FileTypePDF;
                    NewTrainingCourseCategory.AttachPDFUrl = FileUrlPDF;
                }
                else
                {
                    return NotFound();
                }
                //-----------------END AttachPDF-----------------------

                try
                {
                    bool retAdd = BisTrainingCourseCategory.AddTrainingCourseCategory(NewTrainingCourseCategory);
                    if (retAdd)
                    {
                        imageTrainingCourseCategory.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + FileUrlPDF, FileBytes);
                        return Ok(retAdd);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    ViewModel.tblTrainingCourseCategory delete = new ViewModel.tblTrainingCourseCategory();
                    delete.IDTrainingCourseCategory = NewTrainingCourseCategory.IDTrainingCourseCategory;
                    bool retDelete = BisTrainingCourseCategory.DeleteTrainingCourseCategory(delete);
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult UpdateTrainingCourseCategory(object obj)
        {
            string FileUrl = "";
            string FileUrlPDF = "";
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblTrainingCourseCategory UpdateTrainingCourseCategory = JsonObject.ToObject<ViewModel.tblTrainingCourseCategory>();

                ViewModel.Search TrainingCourseCategorySearch = new ViewModel.Search();
                TrainingCourseCategorySearch.Filter = " AND tblTrainingCourseCategory.IDTrainingCourseCategory ='" + UpdateTrainingCourseCategory.IDTrainingCourseCategory + "'";
                TrainingCourseCategorySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryJsonData(TrainingCourseCategorySearch);
                if (UpdateTrainingCourseCategory.PicUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourseCategory[0]["PicUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourseCategory[0]["PicUrl"].ToString());
                }
                if (JsonObject["PicFile"].ToString() != "{}")
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imageTrainingCourseCategory = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        if (imageBytes.Length > 0)
                        {
                            FileUrl = @"Picture\TrainingCourseCategory\" + Guid.NewGuid() + FileType;
                            UpdateTrainingCourseCategory.PicUrl = FileUrl;
                            imageTrainingCourseCategory.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        }
                    }
                }


                if (UpdateTrainingCourseCategory.AttachPDFUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourseCategory[0]["AttachPDFUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourseCategory[0]["AttachPDFUrl"].ToString());
                }
                if (JsonObject["AttachPDFFile"].ToString() != "{}")
                {
                    byte[] FileBytes = Convert.FromBase64String(JsonObject["AttachPDFFile"].ToString().Split(',')[1]);// dataye file ersali
                    MemoryStream msPDF = new MemoryStream(FileBytes, 0,
                      FileBytes.Length);
                    FileTypes ft = new FileTypes();
                    msPDF.Write(FileBytes, 0, FileBytes.Length);
                    string FileTypePDF = ft.FindFileTypeInString(JsonObject["AttachPDFFile"].ToString().Split(',')[0]); // sakhtare file ersali

                    if (FileBytes.Length < 50 * 1024 * 1024)
                    {
                        FileUrlPDF = @"Picture\TrainingCourseCategory\" + Guid.NewGuid() + FileTypePDF;
                        UpdateTrainingCourseCategory.AttachPDFUrl = FileUrlPDF;
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + FileUrlPDF, FileBytes);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                bool retUpdate = BisTrainingCourseCategory.UpdateTrainingCourseCategory(UpdateTrainingCourseCategory);
                if (retUpdate)
                {
                    
                    return Ok(retUpdate);
                }
                else
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + FileUrlPDF);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult DeleteTrainingCourseCategory(ViewModel.tblTrainingCourseCategory DelTrainingCourseCategory)
        {
            try
            {
                ViewModel.Search objSearch = new ViewModel.Search();
                objSearch.Filter = " AND tblTrainingCourseCategory.IDTrainingCourseCategory = '" + DelTrainingCourseCategory.IDTrainingCourseCategory + "'";
                objSearch.JsonResult = " For Json Path";
                JArray JsonTrainingCourseCategory = BisTrainingCourseCategory.GetTrainingCourseCategoryJsonData(objSearch);
                bool ret = BisTrainingCourseCategory.DeleteTrainingCourseCategory(DelTrainingCourseCategory);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonTrainingCourseCategory[0]["PicUrl"]);
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



