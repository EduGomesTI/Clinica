using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Doctors.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers
{
    internal sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, ValueResult>
    {
        private const string QUEUE = "create-doctor";

        private readonly ILogger<CreateDoctorCommandHandler> _logger;
        private readonly IMessageService _message;

        public CreateDoctorCommandHandler(
            ILogger<CreateDoctorCommandHandler> logger,
            IMessageService message)
        {
            _logger = logger;
            _message = message;
        }

        public Task<ValueResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            _message.Publish(request, QUEUE);

            return Task.FromResult(ValueResult.Success());
        }
    }
}