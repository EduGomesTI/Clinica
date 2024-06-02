using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Doctors.Handlers;
using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class SoftDeleteSpecialtyCommandHandler : IRequestHandler<SoftDeleteSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "softDelete-specialty";
        private readonly ILogger<UpdateDoctorCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<SoftDeleteSpecialtyCommand> _validator;

        public SoftDeleteSpecialtyCommandHandler(
            ILogger<UpdateDoctorCommandHandler> logger
            , IMessageService message,
            IValidator<SoftDeleteSpecialtyCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(SoftDeleteSpecialtyCommand request, CancellationToken cancellationToken)
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