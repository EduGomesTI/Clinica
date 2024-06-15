namespace Clinica.Base.Infrastructure.Mail
{
    public sealed record SendMailMessage(string Name, string Email, string Message);
}