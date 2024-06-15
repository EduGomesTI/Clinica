using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Main.Application.Patients.Responses;
using Clinica.Main.Domain.Patients;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class LoginPatientCommandHandler : IRequestHandler<LoginPatientCommand, ValueResult<LoginPatientResponse>>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<LoginPatientCommandHandler> _logger;
        private readonly IValidator<LoginPatientCommand> _validator;

        public LoginPatientCommandHandler(
            IPatientRepository repository,
            ILogger<LoginPatientCommandHandler> logger,
            IValidator<LoginPatientCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ValueResult<LoginPatientResponse>> Handle(LoginPatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Validar request");
            var validator = _validator.Validate(request);
            if (!validator.IsValid)
            {
                _logger.LogError("Erro de validação");
                return await Task.FromResult(ValueResult<LoginPatientResponse>.Failure(ValueErrorDetail.FromValidationFailures(validator.Errors)));
            }

            _logger.LogInformation("Buscar paciente pelo login");
            var patient = await _repository.GetPatientByEmailAndPasswordAsync(request.Email, request.Password, cancellationToken);

            if (patient is null)
            {
                _logger.LogError("Login inválido");
                return await Task.FromResult(ValueResult<LoginPatientResponse>.Failure("Paciente não encontrado"));
            }

            return await Task.FromResult(ValueResult<LoginPatientResponse>.Success(new LoginPatientResponse(patient.Id)));
        }
    }
}