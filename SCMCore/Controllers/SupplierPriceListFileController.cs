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
    public class SupplierPriceListFileController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddSupplierPriceListFile()
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.SupplierPriceListFileMethod BisSupplierPriceList = new Bis.SupplierPriceListFileMethod();

            try
            {
                var File = HttpContext.Current.Request.Files["excelFileUploadSupplierPriceList"];
                string FileType = File.FileName.Substring(File.FileName.LastIndexOf("."));
                var IDSupplierPriceListFile = HttpContext.Current.Request["IDSupplierPriceListFile"];
                var IDLogUser = HttpContext.Current.Request["IDLogUser"];
                var TitleSupplierPriceList = HttpContext.Current.Request["TitleSupplierPriceList"];
                var IDSupplier = HttpContext.Current.Request["IDSupplier"];
                var IDCurrency = HttpContext.Current.Request["IDCurrency"];
                var ExcelJsonSupplierPriceList = HttpContext.Current.Request["ExcelJsonSupplierPriceList"];

                ViewModel.tblSupplierPriceListFile Add = new ViewModel.tblSupplierPriceListFile();
                Add.IDSupplierPriceListFile = IDSupplierPriceListFile.ToString().StringToGuid();
                Add.IDPersonel = AuUser.ReturnIDUser(IDLogUser.ToString().StringToGuid());
                Add.IDSupplier = IDSupplier.ToString().StringToGuid();
                Add.IDCurrency = IDCurrency.ToString().StringToGuid();
                Add.Title = TitleSupplierPriceList.ToString();
      
                Add.FileSize = File.ContentLength; //byte
                Add.FileUrl = @"File\AttachCrm\" + Add.IDSupplierPriceListFile + "@" + File.FileName;
                Add.ExcelJsonSupplierPriceList = ExcelJsonSupplierPriceList.ToString();
                bool ret = BisSupplierPriceList.AddSupplierPriceListFile(Add);
                if (ret)
                {
                    try
                    {
                        File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrl);
                        return Ok(ret);
                    }
                    catch (Exception)
                    {

                        ViewModel.tblSupplierPriceListFile delete = new ViewModel.tblSupplierPriceListFile();
                        delete.IDSupplierPriceListFile = Add.IDSupplierPriceListFile;
                        bool ret2 = BisSupplierPriceList.DeleteSupplierPriceListFile(delete);
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
        public IHttpActionResult UpdateRaioSupplierPriceListFile(object obj)
        {
            try
            {

                Bis.SupplierPriceListFileMethod BisSupplierPriceList = new Bis.SupplierPriceListFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblSupplierPriceListFile Update = JsonObject.ToObject<ViewModel.tblSupplierPriceListFile>();
                bool ret = BisSupplierPriceList.UpdateRatioSupplierPriceListFile(Update);
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
        public IHttpActionResult UpdateSupplierPriceListFile(object obj)
        {
            try
            {
                AuthorizationUser AuUser = new AuthorizationUser();

                Bis.SupplierPriceListFileMethod BisSupplierPriceList = new Bis.SupplierPriceListFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                JsonObject.Add("IDPersonel", AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()));
                ViewModel.tblSupplierPriceListFile Update = JsonObject.ToObject<ViewModel.tblSupplierPriceListFile>();
                bool ret = BisSupplierPriceList.UpdateSupplierPriceListFile(Update);
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
        public IHttpActionResult DeleteSupplierPriceListFile(Object obj)
        {
            try
            {
                JObject json = JObject.Parse(obj.ToString());
                Bis.SupplierPriceListFileMethod BisSupplierPriceList = new Bis.SupplierPriceListFileMethod();
                ViewModel.tblSupplierPriceListFile delete = new ViewModel.tblSupplierPriceListFile();
                delete.IDSupplierPriceListFile = json["IDSupplierPriceListFile"].ToString().StringToGuid();
                bool ret = BisSupplierPriceList.DeleteSupplierPriceListFile(delete);
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
        public IHttpActionResult GetSupplierPriceListFile(object obj)
        {
            try
            {

                Bis.SupplierPriceListFileMethod BisSupplierPriceList = new Bis.SupplierPriceListFileMethod();
                ViewModel.tblSupplierPriceListFile getSupplierPriceList = new ViewModel.tblSupplierPriceListFile();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getSupplierPriceList.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonSupplierPriceList = BisSupplierPriceList.GetSupplierPriceListFile(getSupplierPriceList);
                return Ok(JsonSupplierPriceList);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeSortInSupplierPriceFile(object obj)
        {
            try
            {
                Bis.SupplierPriceListFileMethod BisSuppliertFile = new Bis.SupplierPriceListFileMethod();
                ViewModel.tblSupplierPriceListFile update = new ViewModel.tblSupplierPriceListFile();
                update.JsonSupplierPriceFile = obj.ToString();
                bool ret = BisSuppliertFile.ChangeSortInSupplierPriceFile(update);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
