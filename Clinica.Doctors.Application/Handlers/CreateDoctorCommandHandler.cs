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
    public sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, ValueResult>
    {
        private readonly IDoctorRepository _repository;
        private readonly ILogger<CreateDoctorCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;

        public CreateDoctorCommandHandler(
            IDoctorRepository repository,
            ILogger<CreateDoctorCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating doctor {Name}", request.Name);
            var doctor = Doctor.Create(request.Name, request.CRM, request.BirthDate, request.Email,
                request.Phone, request.Address, request.IdSpecialty);

            if (!doctor.Succeeded)
            {
                return ValueResult.Failure(doctor.ErrorDetails!);
            }

            _logger.LogInformation("Doctor {Name} created", request.Name);
            await _repository.CreateAsync(doctor.Value!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}