using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Main.Application.Patients.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.patient_create;

        private readonly ILogger<CreatePatientCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<CreatePatientCommand> _validator;

        public CreatePatientCommandHandler(
            ILogger<CreatePatientCommandHandler> logger
            , IMessageService message,
            IValidator<CreatePatientCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
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