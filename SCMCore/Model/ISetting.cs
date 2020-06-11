namespace SCMCore.Model
{
    public interface ISetting
    {
        int IDSetting { get; set; }
        string EmailSender { get; set; }
        string SenderPassword { get; set; }
        string EmailReciver { get; set; }
        string SmtpAddress { get; set; }
        string Port { get; set; }
    }
}