using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "create-specialty";
        private readonly ILogger<CreateSpecialtyCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IValidator<CreateSpecialtyCommand> _validator;

        public CreateSpecialtyCommandHandler(
                ILogger<CreateSpecialtyCommandHandler> logger
                , IValidator<CreateSpecialtyCommand> validator,
                IMessageService message)
        {
            _logger = logger;
            _validator = validator;
            _message = message;
        }

        public async Task<ValueResult> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
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