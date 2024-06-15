using Clinica.Base.Infrastructure.Options;
using Clinica.Send_Emails.Infrastructure;
using Clinica.Send_Emails.Infrastructure.Options;
using Clinica.Send_Emails.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.ConfigureOptions<SendMailServiceOptionsSetup>();

builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();

builder.Services.AddInfrastructure();

var host = builder.Build();
host.Run();