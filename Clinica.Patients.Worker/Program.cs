using Clinica.Patients.Infrastructure;
using Clinica.Patients.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

builder.Services.AddInfrastructure();

host.Run();
