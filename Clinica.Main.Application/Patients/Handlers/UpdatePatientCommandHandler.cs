using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Patients.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ValueResult>
    {
        private const string QUEUE = "update-patient";
        private readonly ILogger<UpdatePatientCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<UpdatePatientCommand> _validator;

        public UpdatePatientCommandHandler(
            ILogger<UpdatePatientCommandHandler> logger,
            IMessageService message,
            IValidator<UpdatePatientCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Validar request");
            var validator = _validator.Validate(request);
            if (!validator.IsValid)
            {
                _logger.LogError("Erro de validação");
                return ValueResult.Failure(ValueErrorDetail.FromValidationFailures(validator.Errors));
            }

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            _message.Publish(request, QUEUE);

            return ValueResult.Success();
        }
    }
}