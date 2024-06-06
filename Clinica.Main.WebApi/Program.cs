using Clinica.Base.Infrastructure.Options;
using Clinica.Main.Application;
using Clinica.Main.Presentation;
using Clinica.Main.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<RabbitMqOptionsSetup>();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddRabbitMqExtension();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePresentation();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.Run();