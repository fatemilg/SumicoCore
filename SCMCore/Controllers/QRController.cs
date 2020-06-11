using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.Classes;
using SCMCore.ExtensionMethod;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QRCoder;
using System.Drawing;
using System.IO;

namespace SCMCore.Controllers
{
    public class QRController : ApiController
    {
        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetQRCode(object obj)
        {
            try
            {
                JObject JsonObject = JObject.Parse(obj.ToString());
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(JsonObject["Text"].ToString(), QRCodeGenerator.ECCLevel.H);
                string RetValue;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        RetValue = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                }
                return Ok(RetValue);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}