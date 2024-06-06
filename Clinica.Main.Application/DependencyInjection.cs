using Clinica.Base.Application;
using Clinica.Base.Infrastructure;
using Clinica.Main.Application.Doctors.Commands;
using Clinica.Main.Application.Doctors.Validators;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Main.Application.Patients.Validators;
using Clinica.Main.Application.Schedulings.Commands;
using Clinica.Main.Application.Schedulings.Validators;
using Clinica.Main.Application.Specialty.Commands;
using Clinica.Main.Application.Specialty.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Clinica.Main.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidators();

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IValidator<CreateDoctorCommand>, CreateDoctorCommandValidator>();
            services.AddScoped<IValidator<UpdateDoctorCommand>, UpdateDoctorCommandValidator>();
            services.AddScoped<IValidator<SoftDeleteDoctorCommand>, SoftDeleteDoctorCommandValidator>();

            services.AddScoped<IValidator<CreatePatientCommand>, CreatePatientCommandValidator>();
            services.AddScoped<IValidator<UpdatePatientCommand>, UpdatePatientCommandValidator>();
            services.AddScoped<IValidator<SoftDeletePatientCommand>, SoftDeletePatientCommandValidator>();

            services.AddScoped<IValidator<CreateSchedulingCommand>, CreateSchedulingCommandValidator>();
            services.AddScoped<IValidator<UpdateSchedulingCommand>, UpdateSchedulingCommandValidator>();

            services.AddScoped<IValidator<CreateSpecialtyCommand>, CreateSpecialtyCommandValidator>();
            services.AddScoped<IValidator<UpdateSpecialtyCommand>, UpdateSpecialtyCommandValidator>();
            services.AddScoped<IValidator<SoftDeleteSpecialtyCommand>, SoftDeleteSpecialtyCommandValidator>();

            return services;
        }
    }
}