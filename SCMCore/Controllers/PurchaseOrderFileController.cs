using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using ViewModel = SCMCore.ViewModel;
using SCMCore.Classes;

namespace SCMCore.Controllers
{
    public class PurchaseOrderFileController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddPurchaseOrderFile()
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();

            try
            {
                var File = HttpContext.Current.Request.Files["excelFileUpload"];
                string FileType = File.FileName.Substring(File.FileName.LastIndexOf("."));
                var IDPurchaseOrderFile = HttpContext.Current.Request["IDPurchaseOrderFile"];
                var IDLogUser = HttpContext.Current.Request["IDLogUser"];
                var TitlePurchaseOrderFile = HttpContext.Current.Request["TitlePurchaseOrderFile"];
                var IDSupplier = HttpContext.Current.Request["IDSupplier"];
                var IDCurrency = HttpContext.Current.Request["IDCurrency"];
                var DeliverDate = HttpContext.Current.Request["DeliverDate"];
                var ExcelJson = HttpContext.Current.Request["ExcelJson"];

                ViewModel.tblPurchaseOrderFile Add = new ViewModel.tblPurchaseOrderFile();
                Add.IDPurchaseOrderFile = IDPurchaseOrderFile.ToString().StringToGuid();
                Add.IDPersonel = AuUser.ReturnIDUser(IDLogUser.ToString().StringToGuid());
                Add.IDSupplier = IDSupplier.ToString().StringToGuid();
                Add.IDCurrency = IDCurrency.ToString().StringToGuid();
                Add.Title = TitlePurchaseOrderFile.ToString();
                if (DeliverDate == "undefined")
                {
                    Add.DeliverDate = "";
                }
                else
                {
                    Add.DeliverDate = DeliverDate.ToString();
                }

                Add.FileSize = File.ContentLength; //byte
                Add.FileUrl = @"File\AttachCrm\" + Add.IDPurchaseOrderFile + "@" + File.FileName;
                Add.ExcelJson = ExcelJson.ToString();
                bool ret = BisPurchaseOrderFile.AddPurchaseOrderFile(Add);
                if (ret)
                {
                    try
                    {
                        File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrl);
                        return Ok(ret);
                    }
                    catch (Exception)
                    {

                        ViewModel.tblPurchaseOrderFile delete = new ViewModel.tblPurchaseOrderFile();
                        delete.IDPurchaseOrderFile = Add.IDPurchaseOrderFile;
                        bool ret2 = BisPurchaseOrderFile.DeletePurchaseOrderFile(delete);
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
        public IHttpActionResult UpdatePurchaseOrderFile(object obj)
        {
            try
            {
                AuthorizationUser AuUser = new AuthorizationUser();

                Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                JsonObject.Add("IDPersonel", AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()));
                ViewModel.tblPurchaseOrderFile Update = JsonObject.ToObject<ViewModel.tblPurchaseOrderFile>();
                bool ret = BisPurchaseOrderFile.UpdatePurchaseOrderFile(Update);
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
        public IHttpActionResult UpdateShow(object obj)
        {
            try
            {
                Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPurchaseOrderFile Update = JsonObject.ToObject<ViewModel.tblPurchaseOrderFile>();
                bool ret = BisPurchaseOrderFile.UpdateShow(Update);
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
        public IHttpActionResult DeletePurchaseOrderFile(Object obj)
        {
            try
            {
                JObject json = JObject.Parse(obj.ToString());
                Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();
                ViewModel.tblPurchaseOrderFile delete = new ViewModel.tblPurchaseOrderFile();
                delete.IDPurchaseOrderFile = json["IDPurchaseOrderFile"].ToString().StringToGuid();
                bool ret = BisPurchaseOrderFile.DeletePurchaseOrderFile(delete);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + json["FileUrl"].ToString());
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
        public IHttpActionResult GetPurchaseOrderFileAndDetail(object obj)
        {
            try
            {

                Bis.PurchaseOrderFileMethod BisPurchaseorderFile = new Bis.PurchaseOrderFileMethod();
                ViewModel.tblPurchaseOrderFile getPurchaseOrderFile = new ViewModel.tblPurchaseOrderFile();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPurchaseOrderFile.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonPurchaseOrderFile = BisPurchaseorderFile.GetPurchaseOrderFileAndDetail(getPurchaseOrderFile);
                return Ok(JsonPurchaseOrderFile);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetPurchaseOrderFileInViews(object obj)
        {
            try
            {

                Bis.PurchaseOrderFileMethod BisPurchaseorderFile = new Bis.PurchaseOrderFileMethod();
                ViewModel.Search getPurchaseOrderFile = new ViewModel.Search();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPurchaseOrderFile.Filter = " and tblPurchaseOrderFile.IDSupplier='" + JsonObject["IDSupplier"].ToString() + "' ";
                if (JsonObject["Show"].ToString().StringToBool())
                {
                    getPurchaseOrderFile.Filter += "  and tblPurchaseOrderFile.Show ='" + JsonObject["Show"].ToString() + "'";
                }
         
                getPurchaseOrderFile.Order = " order by tblPurchaseOrderFile.Sort";
                getPurchaseOrderFile.JsonResult = " FOR JSON Path";
                JArray JsonPurchaseOrderFile = BisPurchaseorderFile.GetPurchaseOrderFileData(getPurchaseOrderFile);
                return Ok(JsonPurchaseOrderFile);
            }
            catch
            {
                return NotFound();
            }

        }


       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult CheckPartNumberInPurchaseOrderFile(object obj)
        {
            try
            {
                Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();
                ViewModel.tblPurchaseOrderFile check = new ViewModel.tblPurchaseOrderFile();
                check.JsonPartNumberInFile = obj.ToString();
                JArray ret = BisPurchaseOrderFile.CheckPartNumberInPurchaseOrderFile(check);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }

       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeSortInPurchaseOrderFile(object obj)
        {
            try
            {

                Bis.PurchaseOrderFileMethod BisPurchaseOrderFile = new Bis.PurchaseOrderFileMethod();
                ViewModel.tblPurchaseOrderFile update = new ViewModel.tblPurchaseOrderFile();
                update.JsonPurchaseOrderFile = obj.ToString();
                bool ret = BisPurchaseOrderFile.UpdateSortInPurchaseOrderFile(update);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
