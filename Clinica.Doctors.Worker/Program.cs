using Clinica.Base.Infrastructure.Options;
using Clinica.Doctors.Infrastructure;
using Clinica.Doctors.Infrastructure.Options;
using Clinica.Doctors.Worker;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();

builder.Services.AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Doctors.Application.Handlers.CreateDoctorCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Doctors.Application.Handlers.SoftDeleteDoctorCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Doctors.Application.Handlers.UpdateDoctorCommandHandler).Assembly));

var host = builder.Build();
host.Run();