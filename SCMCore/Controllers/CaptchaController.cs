using Newtonsoft.Json.Linq;
using SCMCore.Classes;
using System;
using System.Web.Http;

namespace SCMCore.Controllers
{
    public class CaptchaController : ApiController
    {
       [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetCaptcha()
        {
            try
            {
                RandomImage CaptchaImageEmail = new RandomImage();
                Tuple<byte[], string> CaptchaEmail = CaptchaImageEmail.CaptchaImage();
                string ImageData = "data:image/jpg;base64," + Convert.ToBase64String(CaptchaEmail.Item1);
                string CaptchaText = CaptchaEmail.Item2;
                JObject JsonObject = new JObject();
                JsonObject.Add("ImageData", ImageData);
                JsonObject.Add("CaptchaText", CaptchaText);
                return Ok(JsonObject);
            }
            catch
            {
                return NotFound();
            }


        }


    }
}