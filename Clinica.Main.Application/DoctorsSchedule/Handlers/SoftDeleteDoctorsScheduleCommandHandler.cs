using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Handlers;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.DoctorsSchedule.Handlers
{
    internal sealed class SoftDeleteDoctorsScheduleCommandHandler : IRequestHandler<SoftDeleteDoctorScheduleCommand, ValueResult>
    {
        private const string QUEUE = "softDelete-doctorSchedule";

        private readonly ILogger<SoftDeleteDoctorsScheduleCommandHandler> _logger;

        public SoftDeleteDoctorsScheduleCommandHandler(
            ILogger<SoftDeleteDoctorsScheduleCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(SoftDeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}