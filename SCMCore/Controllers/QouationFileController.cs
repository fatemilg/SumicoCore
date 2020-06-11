using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;

namespace SCMCore.Controllers
{
    public class QouationFileController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddQouationFile()
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.QouationFileMethod BisQouation = new Bis.QouationFileMethod();

            try
            {
                ViewModel.tblQouationFile Add = new ViewModel.tblQouationFile();
                bool ExcelModeQouation = HttpContext.Current.Request["ExcelModeQouation"].ToString().StringToBool();

                var PdfFile = HttpContext.Current.Request.Files["pdfFileUploadQouation"];
                var IDQouationFile = HttpContext.Current.Request["IDQouationFile"];
                var IDLogUser = HttpContext.Current.Request["IDLogUser"];
                var TitleQouation = HttpContext.Current.Request["TitleQouation"];
                var IDSupplier = HttpContext.Current.Request["IDSupplier"];
                var IDCurrency = HttpContext.Current.Request["IDCurrency"];
                var OrigDate = HttpContext.Current.Request["OrigDate"];
                var EmailFile = HttpContext.Current.Request.Files["EmailFileUploadQouation"];



                Add.IDQouationFile = IDQouationFile.ToString().StringToGuid();
                Add.IDPersonel = AuUser.ReturnIDUser(IDLogUser.ToString().StringToGuid());
                Add.IDSupplier = IDSupplier.ToString().StringToGuid();
                Add.IDCurrency = IDCurrency.ToString().StringToGuid();
                Add.Title = TitleQouation.ToString();
                Add.OrigDate = OrigDate.ToString().StringToDateTime();

                Add.FileSizePdf = PdfFile.ContentLength; //byte
                Add.FileUrlPdf = @"File\AttachCrm\" + Add.IDQouationFile + "@" + PdfFile.FileName;

                if (EmailFile != null)
                {
                    Add.FileSizeEmail = EmailFile.ContentLength; //byte
                    Add.FileUrlEmail = @"File\AttachCrm\" + Add.IDQouationFile + "@" + EmailFile.FileName;
                }
                bool ret = false;
                if (ExcelModeQouation)
                {
                    var ExcelFile = HttpContext.Current.Request.Files["excelFileUploadQouation"];
                    var ExcelJsonQouation = HttpContext.Current.Request["ExcelJsonQouation"];
                    Add.FileSizeExcel = ExcelFile.ContentLength; //byte
                    Add.FileUrlExcel = @"File\AttachCrm\" + Add.IDQouationFile + "@" + ExcelFile.FileName;
                    Add.ExcelJsonQouation = ExcelJsonQouation.ToString();
                    Add.ExcelMode = true;
                    ret = BisQouation.AddQouationFileWithExcel(Add);
                    if (ret)
                    {
                        try
                        {
                            ExcelFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlExcel);
                            PdfFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlPdf);
                            if (EmailFile != null)
                            {
                                EmailFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlEmail);

                            }
                            return Ok(ret);
                        }
                        catch (Exception)
                        {

                            ViewModel.tblQouationFile delete = new ViewModel.tblQouationFile();
                            delete.IDQouationFile = Add.IDQouationFile;
                            bool ret2 = BisQouation.DeleteQouationFile(delete);
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
                    Add.ExcelMode = false;
                    ret = BisQouation.AddQouationFileWithOutExcel(Add);
                    if (ret)
                    {
                        try
                        {
                            PdfFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlPdf);
                            if (EmailFile != null)
                            {
                                EmailFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlEmail);

                            }
                            return Ok(ret);
                        }
                        catch (Exception)
                        {

                            ViewModel.tblQouationFile delete = new ViewModel.tblQouationFile();
                            delete.IDQouationFile = Add.IDQouationFile;
                            bool ret2 = BisQouation.DeleteQouationFile(delete);
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
        public IHttpActionResult UpdateQouationFile(object obj)
        {
            try
            {
                AuthorizationUser AuUser = new AuthorizationUser();

                Bis.QouationFileMethod BisQouation = new Bis.QouationFileMethod();
                ViewModel.tblQouationFile update = new ViewModel.tblQouationFile();
                JObject JsonObject = JObject.Parse(obj.ToString());

                update.IDQouationFile = JsonObject["IDQouationFile"].ToString().StringToGuid();
                update.IDPersonel = AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid());
                update.Title = JsonObject["Title"].ToString();
                update.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                update.IDCurrency = JsonObject["IDCurrency"].ToString().StringToGuid();
                //update.OrigDate = DateTime.Parse(JsonObject["OrigDate"].ToString());
                update.OrigDate = JsonObject["OrigDate"].ToString().StringToDateTime();
                if (JsonObject["NewFileUrlEmail"] == null) //zamane update fili entekhab nashode ast
                {
                    if (JsonObject["OldFileUrlEmail"] != null) //dar lahze aval fili sabt shode ast
                    {
                        update.FileUrlEmail = JsonObject["OldFileUrlEmail"].ToString();
                        update.FileSizeEmail = JsonObject["OldFileSizeEmail"].ToString().StringToInt();
                    }
                }
                else
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + JsonObject["OldFileUrlEmail"].ToString());

                    byte[] FileByte = Convert.FromBase64String(JsonObject["NewFileUrlEmail"].ToString().Split(',')[1]);
                    MemoryStream ms = new MemoryStream(FileByte, 0,
                      FileByte.Length);

                    ms.Write(FileByte, 0, FileByte.Length);
                    FileTypes ft = new FileTypes();
                    string FileType = ft.FindFileTypeInString(JsonObject["NewFileUrlEmail"].ToString().Split(',')[0]);
                    if (FileByte.Length > 0)
                    {
                        string FileUrl = "";
                        string TitleQoutationEmailFile = JsonObject["TitleQoutationEmailFile"].ToString();
                        FileUrl = @"File\AttachCrm\" + Guid.NewGuid() +"@"+ TitleQoutationEmailFile.Split('.')[0];
                        update.FileUrlEmail = FileUrl;
                        update.FileSizeEmail = FileByte.Length;
                        try
                        {
                            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + FileUrl+ FileType, FileByte);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    
                    }
                }

                bool ret = BisQouation.UpdateQouationFile(update);
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
        public IHttpActionResult UpdateRatioQouationFile(object obj)
        {
            try
            {

                Bis.QouationFileMethod BisQouationFile = new Bis.QouationFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblQouationFile Update = JsonObject.ToObject<ViewModel.tblQouationFile>();
                bool ret = BisQouationFile.UpdateRatioQouationFile(Update);
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
        public IHttpActionResult DeleteQouationFile(Object obj)
        {
            try
            {
                JObject json = JObject.Parse(obj.ToString());
                Bis.QouationFileMethod BisQouation = new Bis.QouationFileMethod();
                ViewModel.tblQouationFile delete = new ViewModel.tblQouationFile();
                delete.IDQouationFile = json["IDQouationFile"].ToString().StringToGuid();
                bool ret = BisQouation.DeleteQouationFile(delete);
                if (ret)
                {
                    if (json["FileUrlExcel"] != null)
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + json["FileUrlExcel"].ToString());

                    }
                    if (json["FileUrlEmail"] != null)
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + json["FileUrlEmail"].ToString());

                    }
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + json["FileUrlPdf"].ToString());
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
        public IHttpActionResult GetQouationFile(object obj)
        {
            try
            {

                Bis.QouationFileMethod BisQouation = new Bis.QouationFileMethod();
                ViewModel.tblQouationFile getQouation = new ViewModel.tblQouationFile();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getQouation.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonQouation = BisQouation.GetQouationFile(getQouation);
                return Ok(JsonQouation);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeSortInQouationFile(object obj)
        {
            try
            {
                Bis.QouationFileMethod BisQouationFile = new Bis.QouationFileMethod();
                ViewModel.tblQouationFile update = new ViewModel.tblQouationFile();
                update.JsonQouationFile = obj.ToString();
                bool ret = BisQouationFile.ChangeSortInQouationFile(update);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
