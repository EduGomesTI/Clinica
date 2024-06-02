using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.DoctorsSchedule.Handlers
{
    internal class UpdateDoctorsScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, ValueResult>
    {
        private const string QUEUE = "update-doctorSchedule";
        private readonly ILogger<UpdateDoctorsScheduleCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<UpdateDoctorScheduleCommand> _validator;

        public UpdateDoctorsScheduleCommandHandler(
            ILogger<UpdateDoctorsScheduleCommandHandler> logger,
            IMessageService message,
            IValidator<UpdateDoctorScheduleCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
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