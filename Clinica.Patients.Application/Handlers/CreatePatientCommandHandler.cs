using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Patients.Application.Abstractions;
using Clinica.Patients.Domain.Aggregates;
using Clinica.Patients.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Patients.Application.Handlers
{
    public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ValueResult>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<CreatePatientCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePatientCommandHandler(
            IPatientRepository repository,
            ILogger<CreatePatientCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating patient {Name}", request.Name);
            var patient = Patient.Create(request.Name, request.BirthDate, request.Email,
                request.Phone, request.Adrress);

            if (!patient.Succeeded)
            {
                return ValueResult.Failure(patient.ErrorDetails!);
            }

            _logger.LogInformation("Patient {Name} created", request.Name);
            await _repository.CreateAsync(patient.Value!, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}