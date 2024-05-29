using Clinica.Base.Domain;
using Clinica.Main.Application.Schedulings.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Schedulings.Handlers
{
    internal sealed class CreateSchedulingCommandHandler : IRequestHandler<CreateSchedulingCommand, ValueResult>
    {
        private const string QUEUE = "create-schedule";

        private readonly ILogger<CreateSchedulingCommandHandler> _logger;

        public CreateSchedulingCommandHandler(
            ILogger<CreateSchedulingCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(CreateSchedulingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}