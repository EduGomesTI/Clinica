using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Clinica.Send_Emails.Infrastructure.Options
{
    public sealed class SendMailServiceOptionsSetup : IConfigureOptions<SendMailServiceOptions>
    {
        private readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "SendMailService";

        public SendMailServiceOptionsSetup(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SendMailServiceOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}