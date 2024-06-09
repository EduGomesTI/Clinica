using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Options;
using Clinica.Doctors.Application.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Clinica.Doctors.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(
            IOptions<RabbitMqOptions> options,
            IServiceScopeFactory scopeFactory)
        {
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
            await Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    CreateDoctor(_channel);
                    UpdateDoctor(_channel);
                    DeleteDoctor(_channel);
                    CreateSpecialty(_channel);
                    UpdateSpecialty(_channel);
                    DeleteSpecialty(_channel);
                }
            }, stoppingToken);
        }

        private void CreateDoctor(IModel channel)
        {
            channel.QueueDeclare(
                            queue: MessageConstants.doctor_create,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

            var createDoctor = new EventingBasicConsumer(channel);

            createDoctor.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<CreateDoctorCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.doctor_create,
                        autoAck: true,
                        consumer: createDoctor
                        );
        }

        private void UpdateDoctor(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.doctor_update,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var updateDoctor = new EventingBasicConsumer(channel);

            updateDoctor.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<UpdateDoctorCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.doctor_update,
                        autoAck: true,
                        consumer: updateDoctor
                        );
        }

        private void DeleteDoctor(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.doctor_delete,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var deleteDoctor = new EventingBasicConsumer(channel);

            deleteDoctor.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<SoftDeleteDoctorCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.doctor_delete,
                        autoAck: true,
                        consumer: deleteDoctor
                        );
        }

        private void CreateSpecialty(IModel channel)
        {
            channel.QueueDeclare(
                            queue: MessageConstants.specialty_create,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

            var createSpecialty = new EventingBasicConsumer(channel);

            createSpecialty.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<CreateSpecialtyCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.specialty_create,
                        autoAck: true,
                        consumer: createSpecialty
                        );
        }

        private void UpdateSpecialty(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.specialty_update,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var updateSpecialty = new EventingBasicConsumer(channel);

            updateSpecialty.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<UpdateSpecialtyCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.specialty_update,
                        autoAck: true,
                        consumer: updateSpecialty
                        );
        }

        private void DeleteSpecialty(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.specialty_delete,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var deleteSpecialty = new EventingBasicConsumer(channel);

            deleteSpecialty.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<SoftDeleteSpecialtyCommand>(message);
                using var scope = _scopeFactory.CreateScope();
                var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                mediat.Send(request!);
            };

            channel.BasicConsume(
                        queue: MessageConstants.specialty_delete,
                        autoAck: true,
                        consumer: deleteSpecialty
                        );
        }
    }
}