using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class SoftDeleteDoctorCommandHandler : IRequestHandler<SoftDeleteDoctorCommand, ValueResult>
    {
        private readonly IDoctorRepository _repository;
        private readonly ILogger<SoftDeleteDoctorCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public SoftDeleteDoctorCommandHandler(
            IDoctorRepository repository,
            ILogger<SoftDeleteDoctorCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(SoftDeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando soft delete do médico Id {DoctorId}", request.IdDoctor);

            _logger.LogInformation("Verificando se o médico Id {DoctorId} existe", request.IdDoctor);
            var exists = _repository.Exist(request.IdDoctor);

            if (!exists)
            {
                return ValueResult.Failure("Médico não encontrado");
            }

            if (request.IsDeleted)
            {
                _repository.Delete(request.IdDoctor);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Médico Id {DoctorId} deletado", request.IdDoctor);
            }
            else
            {
                _repository.Undelete(request.IdDoctor);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Médico Id {DoctorId} restaurado", request.IdDoctor);
            }

            return ValueResult.Success();
        }
    }
}