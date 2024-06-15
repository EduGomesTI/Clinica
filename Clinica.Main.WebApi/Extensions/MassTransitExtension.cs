namespace Clinica.Main.WebApi.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var server = configuration["MassTransit:Server"];
            var user = configuration["MassTransit:User"];
            var password = configuration["MassTransit:Password"];

            //services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host(server, "/", h =>
            //        {
            //            h.Username(user!);
            //            h.Password(password!);
            //        });

            //        cfg.ConfigureEndpoints(context);
            //    });
            //});

            return services;
        }
    }
}