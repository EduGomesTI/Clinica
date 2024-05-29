using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers;

internal sealed class SoftDeleteDoctorCommandHandler : IRequestHandler<SoftDeleteDoctorCommand, ValueResult>
{
    private const string QUEUE = "softDelete-doctor";

    private readonly ILogger<SoftDeleteDoctorCommandHandler> _logger;

    public SoftDeleteDoctorCommandHandler(
        ILogger<SoftDeleteDoctorCommandHandler> logger
        )
    {
        _logger = logger;
    }

    public async Task<ValueResult> Handle(SoftDeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Adicionar validação");

        _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");

        return ValueResult.Success();
    }
}