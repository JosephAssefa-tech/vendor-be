namespace Vennderful.Application.Models.Mail
{
    public class EmailSettings
    {
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public string SmtpHost { get; set; } = string.Empty;
        public string SmtpPort { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
    }

}
