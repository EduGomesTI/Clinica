using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Clinica.Base.Infrastructure.Options
{
    public sealed class RabbitMqOptionsSetup : IConfigureOptions<RabbitMqOptions>
    {
        private readonly IConfiguration _configuration;
        private const string SECTIONNAME = "RabbitMq";

        public RabbitMqOptionsSetup(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RabbitMqOptions options)
        {
            _configuration.GetSection(SECTIONNAME).Bind(options);
        }
    }
}