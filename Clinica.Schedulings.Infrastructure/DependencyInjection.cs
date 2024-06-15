using Clinica.Schedulings.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Clinica.Base.Application;
using Clinica.Base.Infrastructure;
using Clinica.Schedulings.Domain.Repositories;
using Clinica.Schedulings.Infrastructure.Persistence;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;

namespace Clinica.Schedulings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<SchedulingDbContext>((serviceprovider, dbContextOptionsBuilder) =>
            {
                var databseOptions = serviceprovider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseNpgsql(databseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databseOptions.MaxRetryCount);

                    options.CommandTimeout(databseOptions.CommandTimeOut);

                    options.MigrationsHistoryTable(databseOptions.MigrationHistoryTable!);
                });

                dbContextOptionsBuilder.LogTo(x => Console.WriteLine(x));

                dbContextOptionsBuilder.EnableDetailedErrors(databseOptions.EnabledDetailedErrors);

                dbContextOptionsBuilder.EnableSensitiveDataLogging(databseOptions.EnabledSensitiveDataLogging);
            });

            services.AddScoped<ISchedulingRepository, SchedulingRepository>();

            services.AddScoped<IUnitOfWork<SchedulingDbContext>, UnitOfWork<SchedulingDbContext>>();

            services.AddScoped<IMessageService, MessageService>();

            return services;
        }
    }
}