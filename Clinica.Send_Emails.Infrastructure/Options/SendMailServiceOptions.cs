namespace Clinica.Send_Emails.Infrastructure.Options
{
    public class SendMailServiceOptions
    {
        public int Port { get; set; }

        public string? Server { get; set; }

        public string? CredentialEmail { get; set; }

        public string? CredentialPassword { get; set; }

        public bool EnableSsl { get; set; }

        public string? From { get; set; }
    }
}