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
    public class PeyvastPriceFileController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult AddPeyvastPriceFile()
        {
            AuthorizationUser AuUser = new AuthorizationUser();
            Bis.PeyvastPriceFileMethod BisPeyvastPrice = new Bis.PeyvastPriceFileMethod();

            try
            {
                var File = HttpContext.Current.Request.Files["excelFileUploadPeyvastPrice"];
                string FileType = File.FileName.Substring(File.FileName.LastIndexOf("."));
                var IDPeyvastPriceFile = HttpContext.Current.Request["IDPeyvastPriceFile"];
                var IDLogUser = HttpContext.Current.Request["IDLogUser"];
                var TitlePeyvastPrice = HttpContext.Current.Request["TitlePeyvastPrice"];
                var IDSupplier = HttpContext.Current.Request["IDSupplier"];
                var IDCurrency = HttpContext.Current.Request["IDCurrency"];
                var OrigDate = HttpContext.Current.Request["OrigDate"];

                var ExcelJsonPeyvastPrice = HttpContext.Current.Request["ExcelJsonPeyvastPrice"];

                ViewModel.tblPeyvastPriceFile Add = new ViewModel.tblPeyvastPriceFile();
                Add.IDPeyvastPriceFile = IDPeyvastPriceFile.ToString().StringToGuid();
                Add.IDPersonel = AuUser.ReturnIDUser(IDLogUser.ToString().StringToGuid());
                Add.IDSupplier = IDSupplier.ToString().StringToGuid();
                Add.IDCurrency = IDCurrency.ToString().StringToGuid();
                Add.Title = TitlePeyvastPrice.ToString();
                Add.OrigDate = OrigDate.ToString().StringToDateTime();


                Add.FileSizeExcel = File.ContentLength; //byte
                Add.FileUrlExcel = @"File\AttachCrm\" + Add.IDPeyvastPriceFile + "@" + File.FileName;
                Add.ExcelJsonPeyvastPrice = ExcelJsonPeyvastPrice.ToString();
                bool ret = BisPeyvastPrice.AddPeyvastPriceFile(Add);
                if (ret)
                {
                    try
                    {
                        File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + Add.FileUrlExcel);
                        return Ok(ret);
                    }
                    catch (Exception)
                    {

                        ViewModel.tblPeyvastPriceFile delete = new ViewModel.tblPeyvastPriceFile();
                        delete.IDPeyvastPriceFile = Add.IDPeyvastPriceFile;
                        bool ret2 = BisPeyvastPrice.DeletePeyvastPriceFile(delete);
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
        public IHttpActionResult UpdatePeyvastPriceFile(object obj)
        {
            try
            {
                AuthorizationUser AuUser = new AuthorizationUser();

                Bis.PeyvastPriceFileMethod BisPeyvastPrice = new Bis.PeyvastPriceFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                JsonObject.Add("IDPersonel", AuUser.ReturnIDUser(JsonObject["IDLogUser"].ToString().StringToGuid()));
                ViewModel.tblPeyvastPriceFile Update = JsonObject.ToObject<ViewModel.tblPeyvastPriceFile>();

                bool ret = BisPeyvastPrice.UpdatePeyvastPriceFile(Update);
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
        public IHttpActionResult UpdateRaioPeyvastPriceFile(object obj)
        {
            try
            {

                Bis.PeyvastPriceFileMethod BisPeyvastPriceFile = new Bis.PeyvastPriceFileMethod();
                JObject JsonObject = JObject.Parse(obj.ToString());
                ViewModel.tblPeyvastPriceFile Update = JsonObject.ToObject<ViewModel.tblPeyvastPriceFile>();
                bool ret = BisPeyvastPriceFile.UpdateRatioPeyvastPriceFile(Update);
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
        public IHttpActionResult DeletePeyvastPriceFile(Object obj)
        {
            try
            {
                JObject json = JObject.Parse(obj.ToString());
                //Bis.PreparationPriceListMethod BisPreparationPriceList = new Bis.PreparationPriceListMethod();
                //ViewModel.tblPreparationPriceList getPreparationByPeyvast = new ViewModel.tblPreparationPriceList();
                //getPreparationByPeyvast.IDPeyvastPriceFile = json["IDPeyvastPriceFile"].ToString().StringToGuid();
                //JArray JsongetPreparationByPeyvast = BisPreparationPriceList.GetPreparationPriceListByIDPeyvastPriceFile(getPreparationByPeyvast);



                Bis.PeyvastPriceFileMethod BisPeyvastPrice = new Bis.PeyvastPriceFileMethod();
                ViewModel.tblPeyvastPriceFile delete = new ViewModel.tblPeyvastPriceFile();
                delete.IDPeyvastPriceFile = json["IDPeyvastPriceFile"].ToString().StringToGuid();
                bool ret = BisPeyvastPrice.DeletePeyvastPriceFile(delete);
                if (ret)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + json["FileUrlExcel"].ToString());
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
        public IHttpActionResult GetPeyvastPriceFile(object obj)
        {
            try
            {

                Bis.PeyvastPriceFileMethod BisPeyvastPrice = new Bis.PeyvastPriceFileMethod();
                ViewModel.tblPeyvastPriceFile getPeyvastPrice = new ViewModel.tblPeyvastPriceFile();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPeyvastPrice.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonPeyvastPrice = BisPeyvastPrice.GetPeyvastPriceFile(getPeyvastPrice);
                return Ok(JsonPeyvastPrice);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetStockValuePeyvast(object obj)
        {
            try
            {

                Bis.PeyvastPriceFileMethod BisPeyvastPrice = new Bis.PeyvastPriceFileMethod();
                ViewModel.tblPeyvastPriceFile getPeyvastPrice = new ViewModel.tblPeyvastPriceFile();

                JObject JsonObject = JObject.Parse(obj.ToString());
                getPeyvastPrice.IDSupplier = JsonObject["IDSupplier"].ToString().StringToGuid();
                JArray JsonPeyvastPrice = BisPeyvastPrice.GetStockValuePeyvast(getPeyvastPrice);
                return Ok(JsonPeyvastPrice);
            }
            catch
            {
                return NotFound();
            }

        }


        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult CheckPartNumberInPeyvastPriceFile(object obj)
        {
            try
            {
                Bis.PeyvastPriceFileMethod BisPeyvastPriceFile = new Bis.PeyvastPriceFileMethod();
                ViewModel.tblPeyvastPriceFile check = new ViewModel.tblPeyvastPriceFile();
                check.JsonPartNumberInFile = obj.ToString();
                JArray ret = BisPeyvastPriceFile.CheckPartNumberInPeyvastPriceFile(check);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult ChangeSortInPeyvastFile(object obj)
        {
            try
            {
                Bis.PeyvastPriceFileMethod BisPeyvastFile = new Bis.PeyvastPriceFileMethod();
                ViewModel.tblPeyvastPriceFile update = new ViewModel.tblPeyvastPriceFile();
                update.JsonPeyvastFile = obj.ToString();
                bool ret = BisPeyvastFile.ChangeSortInPeyvastFile(update);
                return Ok(ret);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
