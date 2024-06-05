using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Options;
using Clinica.Main.Application.Patients.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Clinica.Patients.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IModel _channel;
        private readonly ISender _mediat;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(
            ILogger<Worker> logger,
            IOptions<RabbitMqOptions> options,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            var _options = options;

            var factory = new ConnectionFactory
            {
                HostName = _options.Value.HostName,
                Port = _options.Value.Port,
                UserName = _options.Value.User,
                Password = _options.Value.Password
            };

            var _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CreatePatient(_channel);
                UpdatePatient(_channel);
                DeletePatient(_channel);
            }
        }

        private void DeletePatient(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.patient_delete,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var deletePatient = new EventingBasicConsumer(channel);

            deletePatient.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<SoftDeletePatientCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.patient_delete,
                        autoAck: true,
                        consumer: deletePatient
                        );
        }

        private void UpdatePatient(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.patient_update,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var updatePatient = new EventingBasicConsumer(channel);

            updatePatient.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<UpdatePatientCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.patient_update,
                        autoAck: true,
                        consumer: updatePatient
                        );
        }

        private void CreatePatient(IModel channel)
        {
            channel.QueueDeclare(
                            queue: MessageConstants.patient_create,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

            var createPatient = new EventingBasicConsumer(channel);

            createPatient.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<CreatePatientCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.patient_create,
                        autoAck: true,
                        consumer: createPatient
                        );
        }
    }
}