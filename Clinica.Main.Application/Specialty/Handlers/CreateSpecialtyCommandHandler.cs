using Clinica.Base.Domain;
using Clinica.Main.Application.Specialty.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "create-specialty";

        private readonly ILogger<CreateSpecialtyCommandHandler> _logger;

        public CreateSpecialtyCommandHandler(
            ILogger<CreateSpecialtyCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}