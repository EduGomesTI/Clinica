using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Clinica.Base.Infrastructure.Options
{
    public sealed class MassTransitOptionsSetup : IConfigureOptions<MassTransitOptions>
    {
        private readonly IConfiguration _configuration;
        private const string SECTIONNAME = "MassTransit";

        public MassTransitOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(MassTransitOptions options)
        {
            _configuration.GetSection(SECTIONNAME).Bind(options);
        }
    }
}