using SCMCore.Classes;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using System;
using Newtonsoft.Json.Linq;

namespace SCMCore.Controllers
{
    public class SentEmailController : ApiController
    {
        AuthorizationUser AuUser = new AuthorizationUser();
        Bis.SentEmailMethod BisSentEmail = new Bis.SentEmailMethod();
        Bis.SystemEmailMethod BisSystemEmail = new Bis.SystemEmailMethod();
        Bis.PersonelMethod BisPersonel = new Bis.PersonelMethod();

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult SentTestEmail(ViewModel.tblSentEmail SentEmail)
        {
            try
            {
                ViewModel.tblSystemEmail objSysEmail = new ViewModel.tblSystemEmail();
                DataSet dsSysTemEmail = BisSystemEmail.GetSystemEmailDataSetData(objSysEmail);
                JArray dsUser = AuUser.ReturnUser(SentEmail.IDLogUser);
                SentEmail.EmailFrom = dsSysTemEmail.ReturnDataSetField("Email");
                SentEmail.SmtpAddress = dsSysTemEmail.ReturnDataSetField("SMTP_Address");
                SentEmail.PortNumber = dsSysTemEmail.ReturnDataSetField("PortNumber").StringToInt();
                SentEmail.IDSentEmail = Guid.NewGuid();
                SentEmail.EmailStatus = "Successfull";
                SentEmail.IDSender = dsUser[0]["IDUser"].ToString().StringToGuid();
                SentEmail.SenderFirstName = "";
                SentEmail.SenderLastName = "";

                string NewsLetterStructure = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Templates\NewsLetterStructure.txt");
                SentEmail.Body = NewsLetterStructure.Replace("@@Content", SentEmail.Body).Replace("@@EmailName", SentEmail.EmailTo).Replace("@@IDXContent", SentEmail.IDXRet.ToString());

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SentEmail.EmailFrom, "Farbin");
                mail.To.Add(SentEmail.EmailTo);
                mail.Subject = SentEmail.Subject;
                mail.Body = SentEmail.Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(SentEmail.SmtpAddress, dsSysTemEmail.ReturnDataSetField("PortNumber").StringToInt());
                smtp.Credentials = new NetworkCredential(SentEmail.EmailFrom, dsSysTemEmail.ReturnDataSetField("Password"));
                smtp.EnableSsl = false;
                smtp.Send(mail);
                smtp.Dispose();
                SentEmail.IDLogUser = null;
                bool ret = BisSentEmail.AddSentEmail(SentEmail);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return NotFound();
            }


        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult PreparationForNewsLetter(ViewModel.tblSentEmail SentEmail)
        {
            try
            {
                Guid IDUser = AuUser.ReturnIDUser(SentEmail.IDLogUser);
                ViewModel.Search searchPersonel = new ViewModel.Search();
                searchPersonel.Filter = " AND tblPersonel.IDUser = '" + IDUser + "'";
                DataSet dsPersonel = BisPersonel.GetPersonelData(searchPersonel);

                SentEmail.IDSender = dsPersonel.ReturnDataSetField("IDUser").StringToGuid();
                SentEmail.SenderFirstName = dsPersonel.ReturnDataSetField("FName");
                SentEmail.SenderLastName = dsPersonel.ReturnDataSetField("LName");

                string NewsLetterStructure = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Templates\NewsLetterStructure.txt");
                SentEmail.Body = NewsLetterStructure.Replace("@@Content", SentEmail.Body);
                bool ret = BisSentEmail.PreparationForNewsLetter(SentEmail);

                return Ok(ret);
            }
            catch
            {

                return NotFound();
            }
        }

        [HttpPost, CheckReferrerDomain]
        public IHttpActionResult GetSentEmailByIDRet(ViewModel.tblSentEmail SentEmail)
        {
            try
            {
                JArray Result = BisSentEmail.GetSentEmailJsonData_ByIDRet(SentEmail);
                return Ok(Result);
            }
            catch
            {

                return NotFound();
            }
        }
    }
}