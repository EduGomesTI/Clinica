using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Handlers;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.DoctorsSchedule.Handlers
{
    internal class UpdateDoctorsScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, ValueResult>
    {
        private const string QUEUE = "update-doctorSchedule";

        private readonly ILogger<UpdateDoctorsScheduleCommandHandler> _logger;

        public UpdateDoctorsScheduleCommandHandler(
            ILogger<UpdateDoctorsScheduleCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}