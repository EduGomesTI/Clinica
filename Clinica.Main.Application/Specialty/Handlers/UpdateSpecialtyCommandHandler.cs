using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "update-specialty";
        private readonly ILogger<UpdateSpecialtyCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<UpdateSpecialtyCommand> _validator;

        public UpdateSpecialtyCommandHandler(
            ILogger<UpdateSpecialtyCommandHandler> logger
            , IMessageService message,
            IValidator<UpdateSpecialtyCommand> validator)
        {
            _logger = logger;
            _message = message;
            _validator = validator;
        }

        public async Task<ValueResult> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
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