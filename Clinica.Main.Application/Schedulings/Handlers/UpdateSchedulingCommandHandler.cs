using Clinica.Base.Domain;
using Clinica.Main.Application.Schedulings.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Schedulings.Handlers
{
    internal sealed class UpdateSchedulingCommandHandler : IRequestHandler<UpdateSchedulingCommand, ValueResult>
    {
        private const string QUEUE = "update-scheduling";

        private readonly ILogger<UpdateSchedulingCommandHandler> _logger;

        public UpdateSchedulingCommandHandler(
            ILogger<UpdateSchedulingCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(UpdateSchedulingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}