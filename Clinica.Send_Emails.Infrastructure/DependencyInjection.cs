using Clinica.Send_Emails.Infrastructure.Interfaces;
using Clinica.Send_Emails.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clinica.Send_Emails.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISendMailService, SendMailService>();

            return services;
        }
    }
}