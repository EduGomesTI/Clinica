using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers
{
    internal sealed class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, ValueResult>
    {
        private const string QUEUE = "update-doctor";

        private readonly ILogger<UpdateDoctorCommandHandler> _logger;

        public UpdateDoctorCommandHandler(
            ILogger<UpdateDoctorCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}