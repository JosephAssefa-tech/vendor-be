using System;
using System.Threading.Tasks;
using Vennderful.Application.Models.Mail;

namespace Vennderful.Application.Contracts.Infrastructure.Mail
{
    public interface IEmailService
    {
        Task SendEmail(Email email);
        Task SendEmail(string to, string company, string role, EmailTemplates emailTemplate);
        Task SendEmail(string to, string name, string evnt, Guid eventId, Guid clientId, Vennderful.Application.Models.Mail.EmailTemplates emailTemplate);
    }

}
