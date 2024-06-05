﻿using Clinica.Patients.Infrastructure.Options;
using Clinica.Patients.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Clinica.Base.Application;
using Clinica.Patients.Application.Abstractions;
using Clinica.Patients.Domain.Repositories;

namespace Clinica.Patients.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddDbContext<PatientDbContext>((serviceprovider, dbContextOptionsBuilder) =>
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

            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}