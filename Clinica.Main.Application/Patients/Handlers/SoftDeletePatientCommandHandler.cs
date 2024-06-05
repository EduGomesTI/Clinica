using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Main.Application.Patients.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class SoftDeletePatientCommandHandler : IRequestHandler<SoftDeletePatientCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.patient_delete;

        private readonly ILogger<SoftDeletePatientCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<SoftDeletePatientCommand> _validator;

        public SoftDeletePatientCommandHandler(
            ILogger<SoftDeletePatientCommandHandler> logger
            , IValidator<SoftDeletePatientCommand> validator,
            IMessageService message)
        {
            _logger = logger;
            _validator = validator;
            _message = message;
        }

        public async Task<ValueResult> Handle(SoftDeletePatientCommand request, CancellationToken cancellationToken)
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