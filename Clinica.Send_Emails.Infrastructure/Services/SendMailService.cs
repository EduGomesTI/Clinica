using System.Net.Mail;
using System.Net;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Send_Emails.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Clinica.Send_Emails.Infrastructure.Options;
using Microsoft.Extensions.Logging;

namespace Clinica.Send_Emails.Infrastructure.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly SendMailServiceOptions _emailSettings;
        private readonly ILogger<SendMailService> _logger;

        public SendMailService(
            IOptions<SendMailServiceOptions> emailSettings,
            ILogger<SendMailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(SendMailMessage sendMailMessage)
        {
            _logger.LogInformation("Criando o cliente SMTP");
            var smtpClient = new SmtpClient(_emailSettings.Server, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.CredentialEmail, _emailSettings.CredentialPassword),
                EnableSsl = _emailSettings.EnableSsl,
            };

            _logger.LogInformation("Criando a mensagem de email");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.From!),
                Subject = $"{sendMailMessage.Name}, você tem uma nova mensagem da Clinica",
                Body = sendMailMessage.Message,
            };

            _logger.LogInformation("Adicionando destinatário");
            mailMessage.To.Add(sendMailMessage.Email);

            _logger.LogInformation("Enviando email");
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}