using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;

namespace SCMCore.Controllers
{
    public class PersonelInCompanyController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.PersonelInCompanyMethod BisPersonelInCompany = new Bis.PersonelInCompanyMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPersonelInCompany()
        {
            try
            {
                ViewModel.Search PersonelInCompanySearch = new ViewModel.Search();
                PersonelInCompanySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetPersonelInCompanyJsonData(PersonelInCompanySearch);
                return Ok(JsonPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCompletePersonelInCompanyByIDXForSite(ViewModel.tblPersonelInCompany obj)
        {
            try
            {
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetCompletePersonelInCompanyForSite(obj);
                return Ok(JsonPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCompleteDataByNationalCode(ViewModel.tblPersonelInCompany obj)
        {
            try
            {
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetCompleteDataByNationalCode(obj);
                return Ok(JsonPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPersonelInCompanyByNationalCodeAndFullName(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPersonelInCompany personelInCompany = JsonObject.ToObject<ViewModel.tblPersonelInCompany>();
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetPersonelInCompanyByNationalCodeAndFullName(personelInCompany);
                return Ok(JsonPersonelInCompany);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddPersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPersonelInCompany NewPersonelInCompany = JsonObject.ToObject<ViewModel.tblPersonelInCompany>();

                ViewModel.Search PersonelInCompanySearch = new ViewModel.Search();
                PersonelInCompanySearch.Filter = " AND tblPersonelInCompany.NationalCode = '" + NewPersonelInCompany.NationalCode + "'";
                PersonelInCompanySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetPersonelInCompanyJsonData(PersonelInCompanySearch);
                if (JsonPersonelInCompany.HasValues)
                {
                    return NotFound();
                }

                byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image imagePersonelInCompany = Image.FromStream(ms);
                FileTypes ft = new FileTypes();
                string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);
                if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                {

                    string FileUrl = @"Picture\PersonelInCompany\" + NewPersonelInCompany.IDPersonelInCompany + FileType;

                    NewPersonelInCompany.PicUrl = FileUrl;
                    bool retAdd = BisPersonelInCompany.AddPersonelInCompany(NewPersonelInCompany);
                    if (retAdd)
                    {
                        try
                        {
                            imagePersonelInCompany.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                            return Ok(retAdd);
                        }
                        catch (Exception)
                        {
                            ViewModel.tblPersonelInCompany delete = new ViewModel.tblPersonelInCompany();
                            delete.IDPersonelInCompany = NewPersonelInCompany.IDPersonelInCompany;
                            bool retDelete = BisPersonelInCompany.DeletePersonelInCompany(delete);
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
        public IHttpActionResult UpdatePersonelInCompany(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPersonelInCompany UpdatePersonelInCompany = JsonObject.ToObject<ViewModel.tblPersonelInCompany>();
                string FileUrl = "";
                ViewModel.Search PersonelInCompanySearch = new ViewModel.Search();
                PersonelInCompanySearch.Filter = " AND tblPersonelInCompany.IDPersonelInCompany ='" + UpdatePersonelInCompany.IDPersonelInCompany + "'";
                PersonelInCompanySearch.JsonResult = " FOR JSON PATH ";
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetPersonelInCompanyJsonData(PersonelInCompanySearch);
                if (UpdatePersonelInCompany.PicUrl == "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + JsonPersonelInCompany[0]["PicUrl"].ToString()))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonPersonelInCompany[0]["PicUrl"].ToString());
                }

                if (JsonObject["PicFile"].ToString() != "{}")
                {
                    byte[] imageBytes = Convert.FromBase64String(JsonObject["PicFile"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image imagePersonelInCompany = Image.FromStream(ms);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindImageTypeInString(JsonObject["PicFile"].ToString().Split(',')[0]);

                    if (imageBytes.Length < 1024 * 1024 && ft.IsImage(FileType))
                    {
                        if (imageBytes.Length > 0)
                        {
                            FileUrl = @"Picture\PersonelInCompany\" + Guid.NewGuid() + FileType;
                            UpdatePersonelInCompany.PicUrl = FileUrl;
                            imagePersonelInCompany.Save(AppDomain.CurrentDomain.BaseDirectory + FileUrl);
                        }
                    }
                }


                bool retUpdate = BisPersonelInCompany.UpdatePersonelInCompany(UpdatePersonelInCompany);
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
        public IHttpActionResult DeletePersonelInCompany(ViewModel.tblPersonelInCompany DelPersonelInCompany)
        {
            try
            {
                ViewModel.Search objSearch = new ViewModel.Search();
                objSearch.Filter = " AND tblPersonelInCompany.IDPersonelInCompany = '" + DelPersonelInCompany.IDPersonelInCompany + "'";
                objSearch.JsonResult = " For Json Path";
                JArray JsonPersonelInCompany = BisPersonelInCompany.GetPersonelInCompanyJsonData(objSearch);
                bool ret = BisPersonelInCompany.DeletePersonelInCompany(DelPersonelInCompany);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonPersonelInCompany[0]["PicUrl"]);
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



