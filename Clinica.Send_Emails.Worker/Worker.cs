using System.Text;
using Clinica.Base.Infrastructure.Brokes;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Base.Infrastructure.Options;
using Clinica.Send_Emails.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Clinica.Send_Emails.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IModel _channel;
        private readonly ISendMailService _sendMailService;

        public Worker(
            ISendMailService sendMailService,
            IOptions<RabbitMqOptions> options)
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
            _sendMailService = sendMailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    SendMail(_channel);
                }
            }, stoppingToken);
        }

        private void SendMail(IModel channel)
        {
            channel.QueueDeclare(
                            queue: MessageConstants.clinic_mail_service,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

            var sendMail = new EventingBasicConsumer(channel);

            sendMail.Received += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var request = JsonConvert.DeserializeObject<MessagePayload<SendMailMessage>>(message);
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
                    await _sendMailService.SendEmailAsync(request!.Payload);
                });
            };

            channel.BasicConsume(
                        queue: MessageConstants.clinic_mail_service,
                        autoAck: true,
                        consumer: sendMail
                        );
        }
    }
}