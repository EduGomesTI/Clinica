using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Patients.Domain.Aggregates;
using Clinica.Patients.Infrastructure.Persistence;
using Clinica.Patients.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Base.Infrastructure.Brokes;

namespace Clinica.Patients.Application.Handlers
{
    public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly IPatientRepository _repository;
        private readonly ILogger<CreatePatientCommandHandler> _logger;
        private readonly IUnitOfWork<PatientDbContext> _unitOfWork;
        private readonly IMessageService _message;

        public CreatePatientCommandHandler(
            IPatientRepository repository,
            ILogger<CreatePatientCommandHandler> logger,
            IUnitOfWork<PatientDbContext> unitOfWork,
            IMessageService message)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _message = message;
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

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            var createdMessage = $"Olá {patient.Value!.Name}. Seja bem-vindo!";
            var mailPatient = new SendMailMessage(patient.Value!.Name!, patient.Value!.Email!, createdMessage);
            var message = new MessagePayload<SendMailMessage>(mailPatient);
            _message.Publish(message, QUEUE);

            _logger.LogInformation("Saving changes");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}