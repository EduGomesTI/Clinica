using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class SoftDeleteSpecialtyCommandHandler : IRequestHandler<SoftDeleteSpecialtyCommand, ValueResult>
    {
        private readonly ISpecialtyRepository _repository;
        private readonly ILogger<SoftDeleteSpecialtyCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public SoftDeleteSpecialtyCommandHandler(
            ISpecialtyRepository repository,
            ILogger<SoftDeleteSpecialtyCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(SoftDeleteSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando soft delete da especialidade Id {Id}", request.Id);

            _logger.LogInformation("Verificando se a especialidade Id {Id} existe", request.Id);
            var exists = _repository.Exist(request.Id);

            if (!exists)
            {
                return ValueResult.Failure("Especialidade não encontrado");
            }

            if (request.IsDeleted)
            {
                _repository.Delete(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Especialidade Id {Id} deletado", request.Id);
            }
            else
            {
                _repository.Undelete(request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Especialidade Id {Id} restaurado", request.Id);
            }

            return ValueResult.Success();
        }
    }
}