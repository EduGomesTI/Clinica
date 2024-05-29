using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Handlers;
using Clinica.Main.Application.Specialty.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Specialty.Handlers
{
    internal sealed class SoftDeleteSpecialtyCommandHandler : IRequestHandler<SoftDeleteSpecialtyCommand, ValueResult>
    {
        private const string QUEUE = "softDelete-specialty";

        private readonly ILogger<UpdateDoctorCommandHandler> _logger;

        public SoftDeleteSpecialtyCommandHandler(
            ILogger<UpdateDoctorCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(SoftDeleteSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}