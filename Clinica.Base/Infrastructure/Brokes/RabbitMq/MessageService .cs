using Clinica.Base.Infrastructure.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Clinica.Base.Infrastructure.Brokes.RabbitMq
{
    public sealed class MessageService : IMessageService
    {
        private readonly IConnection _conn;
        private readonly IOptions<RabbitMqOptions> _options;

        public MessageService(IOptions<RabbitMqOptions> options)
        {
            Console.WriteLine("about to connect to rabbit");

            _options = options;

            var factory = new ConnectionFactory
            {
                HostName = _options.Value.HostName,
                Port = _options.Value.Port,
                UserName = _options.Value.User,
                Password = options.Value.Password
            };

            _conn = factory.CreateConnection();
        }

        public void Publish<T>(T message, string queue)
        {
            using var channel = _conn.CreateModel();
            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            string? stringMessage = message!.ToString();

            var byteArray = Encoding.UTF8.GetBytes(stringMessage!);

            channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: byteArray
            );
        }
    }
}