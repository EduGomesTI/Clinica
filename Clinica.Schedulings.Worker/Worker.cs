using System.Text;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Options;
using Clinica.Schedulings.Application.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Clinica.Schedulings.Worker
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
                    CreateScheduling(_channel);
                    UpdateScheduling(_channel);
                }
            }, stoppingToken);
        }

        private void CreateScheduling(IModel channel)
        {
            channel.QueueDeclare(
                            queue: MessageConstants.scheduling_create,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

            var createScheduling = new EventingBasicConsumer(channel);

            createScheduling.Received += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<CreateSchedulingCommand>(message);

                var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

                await retryPolicy.ExecuteAsync(async () =>
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                    await mediat.Send(request!);
                });
            };

            channel.BasicConsume(
                        queue: MessageConstants.scheduling_create,
                        autoAck: true,
                        consumer: createScheduling
                        );
        }

        private void UpdateScheduling(IModel channel)
        {
            channel.QueueDeclare(
                        queue: MessageConstants.scheduling_update,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

            var updateScheduling = new EventingBasicConsumer(channel);

            updateScheduling.Received += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<UpdateSchedulingCommand>(message);

                var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                });

                await retryPolicy.ExecuteAsync(async () =>
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediat = scope.ServiceProvider.GetRequiredService<ISender>();
                    await mediat.Send(request!);
                });
            };

            channel.BasicConsume(
                        queue: MessageConstants.scheduling_update,
                        autoAck: true,
                        consumer: updateScheduling
                        );
        }
    }
}