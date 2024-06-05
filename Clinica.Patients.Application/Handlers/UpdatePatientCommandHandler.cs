﻿using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Patients.Application.Abstractions;
using Clinica.Patients.Domain.Aggregates;
using Clinica.Patients.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Patients.Application.Handlers
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ValueResult>
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<UpdatePatientCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientCommandHandler(
            IPatientRepository repository,
            ILogger<UpdatePatientCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificnado se este paciente existe");
            var patient = _repository.FindPatient(request.Id);

            if (patient is null)
            {
                _logger.LogError("Paciente não encontrado");
                return ValueResult.Failure("Paciente não encontrado");
            }

            _logger.LogInformation("Updating patient {Name}", request.Name);
            patient.UpdateName(request.Name);
            patient.UpdateBirthDate(request.BirthDate);
            patient.UpdateEmail(request.Email);
            patient.UpdatePhone(request.Phone);
            patient.UpdateAddress(request.Address);

            _repository.Update(patient!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}