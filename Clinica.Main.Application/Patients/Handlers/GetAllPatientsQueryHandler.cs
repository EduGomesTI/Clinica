using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Queries;
using Clinica.Main.Application.Patients.Responses;
using Clinica.Main.Domain.Patients;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Patients.Handlers
{
    internal sealed class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, ValueResult<IEnumerable<GetPatientResponse>>>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<GetAllPatientsQueryHandler> _logger;

        public GetAllPatientsQueryHandler(
            IPatientRepository repository,
            ILogger<GetAllPatientsQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ValueResult<IEnumerable<GetPatientResponse>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retorna todos os pacientes paginados");
            var patients = await _repository.GetAllPatientsAsync(cancellationToken, request.Page, request.PageStart, request.PageSize);

            if (patients is null)
            {
                const string errorMessage = "Nenhum paciente encontrado";
                _logger.LogWarning(errorMessage);
                return ValueResult<IEnumerable<GetPatientResponse>>.Failure(errorMessage);
            }

            var patientsResponse = patients.Select(p => new GetPatientResponse
            (p.Id, p.Name!, p.BirthDate, p.Email!, p.Phone!, p.Address!));

            return ValueResult<IEnumerable<GetPatientResponse>>.Success(patientsResponse);
        }
    }
}