using Clinica.Base.Domain;
using Clinica.Main.Application.Specialty.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "update-specialty";

        private readonly ILogger<UpdateSpecialtyCommandHandler> _logger;

        public UpdateSpecialtyCommandHandler(
            ILogger<UpdateSpecialtyCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}