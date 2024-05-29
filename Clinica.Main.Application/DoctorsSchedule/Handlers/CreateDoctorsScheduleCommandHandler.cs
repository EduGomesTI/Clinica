using Clinica.Base.Domain;
using Clinica.Main.Application.DoctorsSchedule.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.DoctorsSchedule.Handlers
{
    internal sealed class CreateDoctorsScheduleCommandHandler : IRequestHandler<CreateDoctorScheduleCommand, ValueResult>
    {
        private const string QUEUE = "create-doctorSchedule";

        private readonly ILogger<CreateDoctorsScheduleCommandHandler> _logger;

        public CreateDoctorsScheduleCommandHandler(
            ILogger<CreateDoctorsScheduleCommandHandler> logger
            )
        {
            _logger = logger;
        }

        public async Task<ValueResult> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Adicionar validação");

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

            return ValueResult.Success();
        }
    }
}