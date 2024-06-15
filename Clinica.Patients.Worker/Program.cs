using System.Reflection;
using Clinica.Base.Infrastructure.Options;
using Clinica.Patients.Infrastructure;
using Clinica.Patients.Infrastructure.Options;
using Clinica.Patients.Worker;
using Microsoft.AspNetCore.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();

builder.Services.AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Patients.Application.Handlers.CreatePatientCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Patients.Application.Handlers.SoftDeletePatientCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Patients.Application.Handlers.UpdatePatientCommandHandler).Assembly));

var host = builder.Build();

host.Run();