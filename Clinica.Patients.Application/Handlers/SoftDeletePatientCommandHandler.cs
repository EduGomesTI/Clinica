using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Patients.Domain.Repositories;
using Clinica.Patients.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Patients.Application.Handlers
{
    public sealed class SoftDeletePatientCommandHandler : IRequestHandler<SoftDeletePatientCommand, ValueResult>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<SoftDeletePatientCommandHandler> _logger;
        private readonly IUnitOfWork<PatientDbContext> _unitOfWork;

        public SoftDeletePatientCommandHandler(
            IPatientRepository repository,
            ILogger<SoftDeletePatientCommandHandler> logger,
            IUnitOfWork<PatientDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(SoftDeletePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando soft delete do paciente Id {PatientId}", request.Id);

            _logger.LogInformation("Verificando se o paciente Id {PatientId} existe", request.Id);
            var exists = _repository.Exist(request.Id);

            if (!exists)
            {
                return ValueResult.Failure("Paciente não encontrado");
            }

            if (request.IsDeleted)
            {
                _repository.Delete(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Paciente Id {PatientId} deletado", request.Id);
            }
            else
            {
                _repository.Undelete(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Paciente Id {PatientId} restaurado", request.Id);
            }

            return ValueResult.Success();
        }
    }
}