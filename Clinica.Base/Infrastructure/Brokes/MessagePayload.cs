namespace Clinica.Base.Infrastructure.Brokes
{
    public class MessagePayload<T>
    {
        public Guid CorrelationId { get; set; }
        public DateTime TimeStamp { get; set; }
        public T Payload { get; set; }

        public MessagePayload(T payload)
        {
            CorrelationId = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Payload = payload;
        }
    }
}