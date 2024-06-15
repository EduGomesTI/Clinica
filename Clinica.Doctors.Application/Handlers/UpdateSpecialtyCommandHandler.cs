using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, ValueResult>
    {
        private readonly ISpecialtyRepository _repository;
        private readonly ILogger<UpdateSpecialtyCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public UpdateSpecialtyCommandHandler(
            ISpecialtyRepository repository,
            ILogger<UpdateSpecialtyCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se esta especilaidade existe");
            var specialty = _repository.Find(request.Id);

            if (specialty is null)
            {
                _logger.LogError("Especialidade não encontrado");
                return ValueResult.Failure("Especialidade não encontrado");
            }

            _logger.LogInformation("Atualizando especialidade {Name}", request.Specialty);
            specialty.UpdateDescription(request.Specialty);

            _repository.Update(specialty!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}