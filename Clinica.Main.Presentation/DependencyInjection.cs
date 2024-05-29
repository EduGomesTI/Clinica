using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Clinica.Main.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddCarter();

            return services;
        }

        public static WebApplication UsePresentation(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}