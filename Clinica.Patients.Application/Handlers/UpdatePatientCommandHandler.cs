using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Main.Application.Patients.Commands;
using Clinica.Patients.Domain.Repositories;
using Clinica.Patients.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Patients.Application.Handlers
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly IPatientRepository _repository;
        private readonly ILogger<UpdatePatientCommandHandler> _logger;
        private readonly IUnitOfWork<PatientDbContext> _unitOfWork;
        private readonly IMessageService _message;

        public UpdatePatientCommandHandler(
            IPatientRepository repository,
            ILogger<UpdatePatientCommandHandler> logger,
            IUnitOfWork<PatientDbContext> unitOfWork,
            IMessageService message)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _message = message;
        }

        public async Task<ValueResult> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Verificando se este paciente existe");
            var patient = _repository.Find(request.Id);

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

            _logger.LogInformation("Patient {Name} updated", request.Name);
            _repository.Update(patient!);

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            var updatedMessage = $"Olá {patient.Name}. Seus dados foram atualizados!";
            var mailPatient = new SendMailMessage(patient.Name!, patient.Email!, updatedMessage);
            var message = new MessagePayload<SendMailMessage>(mailPatient);
            _message.Publish(message, QUEUE);

            _logger.LogInformation("Salvando alterações");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}