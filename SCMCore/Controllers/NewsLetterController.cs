using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
namespace SCMCore.Controllers
{
    public class NewsLetterController : ApiController
    {
        PublicFunctions pbFunctions = new PublicFunctions();
        Bis.ContactWayMethod BisContactWay = new Bis.ContactWayMethod();
        Bis.BulkEmailMethod BisBulkEmail = new Bis.BulkEmailMethod();
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetListOfNewsLetter()
        {
            try
            {
                string[] directories = pbFunctions.ListFiles("ftp://farbin.ir/", "NewsLetter", "New@1398");
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult Unsubscribe(ViewModel.tblBulkEmail BulkEmail)
        {
            try
            {
                bool ret = false;
                {
                    ViewModel.Search searchBulk = new ViewModel.Search();
                    searchBulk.Filter = " AND Email = '" + BulkEmail.Email + "'";
                    searchBulk.JsonResult = " FOR JSON PATH";
                    JArray jsBulkEmail = BisBulkEmail.GetSentEmailData(searchBulk);
                    if (jsBulkEmail.HasValues)
                    {
                        ret = BisBulkEmail.Unsubscribe_To_True(BulkEmail);
                    }
                }
                {
                    ViewModel.Search searchContactWay = new ViewModel.Search();
                    searchContactWay.Filter = " AND tblContactWay.Input = '" + BulkEmail.Email + "'";
                    searchContactWay.JsonResult = " FOR JSON PATH";
                    JArray jsContactWay = BisContactWay.GetContactWayJsonData(searchContactWay);
                    if (jsContactWay.HasValues)
                    {
                        ViewModel.tblContactWay ContactWay = new ViewModel.tblContactWay();
                        ContactWay.Input = BulkEmail.Email;
                        ret = BisContactWay.Unsubscribe_To_True(ContactWay);
                    }

                }
                if (ret)
                {
                    return Ok();
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