using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.DoctorsSchedule.Handlers
{
    internal sealed class SoftDeleteDoctorsScheduleCommandHandler : IRequestHandler<SoftDeleteDoctorScheduleCommand, ValueResult>
    {
        private const string QUEUE = "softDelete-doctorSchedule";
        private readonly ILogger<SoftDeleteDoctorsScheduleCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<SoftDeleteDoctorScheduleCommand> _validator;

        public SoftDeleteDoctorsScheduleCommandHandler(
            ILogger<SoftDeleteDoctorsScheduleCommandHandler> logger,
            IMessageService message,
            IValidator<SoftDeleteDoctorScheduleCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(SoftDeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
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