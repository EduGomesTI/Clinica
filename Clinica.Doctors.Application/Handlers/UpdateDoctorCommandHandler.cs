using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, ValueResult>
    {
        private readonly IDoctorRepository _repository;
        private readonly ILogger<UpdateDoctorCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public UpdateDoctorCommandHandler(
            IDoctorRepository repository,
            ILogger<UpdateDoctorCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se este médico existe");
            var doctor = _repository.Find(request.Id);

            if (doctor is null)
            {
                _logger.LogError("Médico não encontrado");
                return ValueResult.Failure("Médico não encontrado");
            }

            _logger.LogInformation("Updating doctor {Name}", request.Name);
            doctor.UpdateName(request.Name);
            doctor.UpdateBirthDate(request.BirthDate);
            doctor.UpdateEmail(request.Email);
            doctor.UpdatePhone(request.Phone);
            doctor.UpdateAddress(request.Address);
            doctor.UpdateSpecialty(request.IdSpecialty);

            _repository.Update(doctor!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}