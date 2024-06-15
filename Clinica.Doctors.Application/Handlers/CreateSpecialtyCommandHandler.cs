using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Aggregates;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, ValueResult>
    {
        private readonly ISpecialtyRepository _repository;
        private readonly ILogger<CreateSpecialtyCommand> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public CreateSpecialtyCommandHandler(
            ISpecialtyRepository repository,
            ILogger<CreateSpecialtyCommand> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Criando Especialidade {Name}", request.Specialty);
            var specialty = Specialty.Create(request.Specialty);

            if (!specialty.Succeeded)
            {
                return ValueResult.Failure(specialty.ErrorDetails!);
            }

            _logger.LogInformation("Especialidade {Name} criada", request.Specialty);
            await _repository.CreateAsync(specialty.Value!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}