using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Queries;
using Clinica.Main.Application.Patients.Responses;
using Clinica.Main.Domain.Patients;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, ValueResult<GetPatientResponse>>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<GetPatientByIdQueryHandler> _logger;

        public GetPatientByIdQueryHandler(IPatientRepository repository, ILogger<GetPatientByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ValueResult<GetPatientResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retorna um paciente pelo Id");
            var patient = await _repository.GetPatientByIdAsync(request.Id, cancellationToken);

            if (patient is null)
            {
                const string errorMessage = "Paciente não encontrado";
                _logger.LogWarning(errorMessage);
                return ValueResult<GetPatientResponse>.Failure(errorMessage);
            }

            var patientResponse = new GetPatientResponse(
                patient.Id, patient.Name!, patient.BirthDate, patient.Email!, patient.Phone!, patient.Address!);

            return ValueResult<GetPatientResponse>.Success(patientResponse);
        }
    }
}