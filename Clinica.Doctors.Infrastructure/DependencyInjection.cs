using Clinica.Base.Application;
using Clinica.Base.Infrastructure;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Options;
using Clinica.Doctors.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Clinica.Doctors.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                       this IServiceCollection services)
        {
            services.AddDbContext<DoctorDbContext>((serviceprovider, dbContextOptionsBuilder) =>
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

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

            services.AddScoped<IUnitOfWork<DoctorDbContext>, UnitOfWork<DoctorDbContext>>();

            return services;
        }
    }
}