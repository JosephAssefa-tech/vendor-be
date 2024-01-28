using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Models.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace Vennderful.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(Email email)
        {
            // create message
            var emailMime = new MimeMessage();
            emailMime.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
            emailMime.To.Add(MailboxAddress.Parse(email.To));
            emailMime.Subject = email.Subject;
            emailMime.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpHost, int.Parse(_emailSettings.SmtpPort), SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            smtp.Send(emailMime);
            smtp.Disconnect(true);
        }
        public async Task SendEmail(string to, string company, string role, Vennderful.Application.Models.Mail.EmailTemplates emailTemplate)
        {
            var emailMime = new MimeMessage();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpHost, int.Parse(_emailSettings.SmtpPort), SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            switch (emailTemplate)
            {
                case Vennderful.Application.Models.Mail.EmailTemplates.Invitation:
                    emailMime.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
                    emailMime.To.Add(MailboxAddress.Parse(to));
                    emailMime.Subject = EmailTemplates.Invitation_Subject;
                    emailMime.Body = new TextPart(TextFormat.Html) { Text = EmailTemplates.Invitation_Body(company, role) };

                    smtp.Send(emailMime);
                    smtp.Disconnect(true);
                    break;
            }
            
        }
        public async Task SendEmail(string to, string name, string evnt, Guid eventId, Guid clientId, Vennderful.Application.Models.Mail.EmailTemplates emailTemplate)
        {
            var emailMime = new MimeMessage();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpHost, int.Parse(_emailSettings.SmtpPort), SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            switch (emailTemplate)
            {
                case Vennderful.Application.Models.Mail.EmailTemplates.EventInvitation:
                    emailMime.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
                    emailMime.To.Add(MailboxAddress.Parse(to));
                    emailMime.Subject = EmailTemplates.EventInvitation_Subject;
                    emailMime.Body = new TextPart(TextFormat.Html) { Text = EmailTemplates.EventInvitation_Body(name, evnt, eventId, clientId) };

                    smtp.Send(emailMime);
                    smtp.Disconnect(true);
                    break;
            }

        }
    }
}
