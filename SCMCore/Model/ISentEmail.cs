using System;

namespace SCMCore.Model
{
    public interface ISentEmail
    {
        Guid? IDSentEmail { get; set; }
        Guid? IDSender { get; set; }
        Guid? IDRet { get; set; }
        string SmtpAddress { get; set; }
        int? PortNumber { get; set; }
        string EmailFrom { get; set; }
        string EmailTo { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string SenderFirstName { get; set; }
        string SenderLastName { get; set; }
        string EmailStatus { get; set; }
    }
}