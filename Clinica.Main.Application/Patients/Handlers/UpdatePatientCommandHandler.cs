using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ValueResult>
    {
        private const string QUEUE = "update-patient";

        private readonly ILogger<UpdatePatientCommandHandler> _logger;

        public UpdatePatientCommandHandler(ILogger<UpdatePatientCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}