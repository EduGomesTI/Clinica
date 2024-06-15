using Clinica.Schedulings.Infrastructure;
using Clinica.Base.Infrastructure.Options;
using Clinica.Schedulings.Worker;
using Clinica.Schedulings.Infrastructure.Options;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();

builder.Services.AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Schedulings.Application.Handlers.CreateSchedulingCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Clinica.Schedulings.Application.Handlers.UpdateSchedulingCommandHandler).Assembly));

var host = builder.Build();
host.Run();