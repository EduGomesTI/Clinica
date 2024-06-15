using Clinica.Base.Infrastructure.Brokes.RabbitMq;

namespace Clinica.Main.WebApi.Extensions
{
    public static class RabbitMqExtension
    {
        public static IServiceCollection AddRabbitMqExtension(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();

            return services;
        }
    }
}