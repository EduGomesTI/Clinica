using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Main.Application.Doctors.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers
{
    internal sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.doctor_create;
        private readonly ILogger<CreateDoctorCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<CreateDoctorCommand> _validator;

        public CreateDoctorCommandHandler(
            ILogger<CreateDoctorCommandHandler> logger,
            IMessageService message,
            IValidator<CreateDoctorCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
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

            return await Task.FromResult(ValueResult.Success());
        }
    }
}