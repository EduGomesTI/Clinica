using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Main.Application.Doctors.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers;

internal sealed class SoftDeleteDoctorCommandHandler : IRequestHandler<SoftDeleteDoctorCommand, ValueResult>
{
    private const string QUEUE = MessageConstants.doctor_delete;
    private readonly ILogger<SoftDeleteDoctorCommandHandler> _logger;
    private readonly IMessageService _message;
    private readonly IValidator<SoftDeleteDoctorCommand> _validator;

    public SoftDeleteDoctorCommandHandler(
        ILogger<SoftDeleteDoctorCommandHandler> logger
        , IMessageService message,
IValidator<SoftDeleteDoctorCommand> validator)
    {
        _logger = logger;
        _message = message;
        _validator = validator;
    }

    public async Task<ValueResult> Handle(SoftDeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Validar request");
        var validator = _validator.Validate(request);
        if (!validator.IsValid)
        {
            _logger.LogError("Erro de validação");
            return ValueResult.Failure(ValueErrorDetail.FromValidationFailures(validator.Errors));
        }

        _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
        _message.Publish(request, QUEUE);

        return await Task.FromResult(ValueResult.Success());
    }
}