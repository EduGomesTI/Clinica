using Clinica.Base.Infrastructure.Mail;

namespace Clinica.Send_Emails.Infrastructure.Interfaces
{
    public interface ISendMailService
    {
        Task SendEmailAsync(SendMailMessage sendMailMessage);
    }
}