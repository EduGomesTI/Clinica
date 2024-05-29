using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class SoftDeletePatientCommandHandler : IRequestHandler<SoftDeletePatientCommand, ValueResult>
    {
        private const string QUEUE = "softDelete-patient";

        private readonly ILogger<SoftDeletePatientCommandHandler> _logger;

        public SoftDeletePatientCommandHandler(
            ILogger<SoftDeletePatientCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(SoftDeletePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}