using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ValueResult>
    {
        private const string QUEUE = "create-patient";

        private readonly ILogger<CreatePatientCommandHandler> _logger;

        public CreatePatientCommandHandler(
            ILogger<CreatePatientCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}