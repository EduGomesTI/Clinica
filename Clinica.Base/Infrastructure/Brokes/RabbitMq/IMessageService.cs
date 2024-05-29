namespace Clinica.Base.Infrastructure.Brokes.RabbitMq
{
    public interface IMessageService
    {
        void Publish<T>(T message, string queue);
    }
}