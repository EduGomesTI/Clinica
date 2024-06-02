using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Schedulings.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Schedulings.Handlers
{
    internal sealed class UpdateSchedulingCommandHandler : IRequestHandler<UpdateSchedulingCommand, ValueResult>
    {
        private const string QUEUE = "update-scheduling";
        private readonly ILogger<UpdateSchedulingCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<UpdateSchedulingCommand> _validator;

        public UpdateSchedulingCommandHandler(
            ILogger<UpdateSchedulingCommandHandler> logger
            , IMessageService message,
            IValidator<UpdateSchedulingCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(UpdateSchedulingCommand request, CancellationToken cancellationToken)
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