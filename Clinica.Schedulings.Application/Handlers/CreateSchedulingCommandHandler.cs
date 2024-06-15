using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Schedulings.Application.Commands;
using Clinica.Schedulings.Domain.Aggregates;
using Clinica.Schedulings.Domain.Repositories;
using Clinica.Schedulings.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Schedulings.Application.Handlers
{
    public sealed class CreateSchedulingCommandHandler : IRequestHandler<CreateSchedulingCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly ILogger<CreateSchedulingCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly ISchedulingRepository _repository;
        private readonly IUnitOfWork<SchedulingDbContext> _unitOfWork;

        public CreateSchedulingCommandHandler(
            ILogger<CreateSchedulingCommandHandler> logger
            , IMessageService message,
ISchedulingRepository repository,
IUnitOfWork<SchedulingDbContext> unitOfWork)
        {
            _logger = logger;
            _message = message;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(CreateSchedulingCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<ValueErrorDetail>();
            var createdMessage = string.Empty;

            _logger.LogInformation("Criando agendamento para o paciente {PatientId}", request.PatientId);
            var scheduling = Scheduling.Create(request.PatientId, request.DoctorId, request.DateScheduling);
            if (!scheduling.Succeeded)
            {
                errors.AddRange(scheduling.ErrorDetails!);
                createdMessage = "Erro ao criar o agendamento.";
            }

            _logger.LogInformation("Verificar se o paciente já tem agendamento para o mesmo dia");
            var patient = await _repository.GetPatientById(request.PatientId, cancellationToken);
            var patientAlreadyScheduled = await _repository.PatientAlreadyScheduled(request.PatientId, scheduling.Value!.DateScheduling, cancellationToken);
            if (patientAlreadyScheduled)
            {
                var errorMessage = $"O paciente {patient!.Name} já tem um agendamento para o dia e horário: {scheduling.Value!.DateScheduling}.";
                _logger.LogInformation(errorMessage);
                errors.Add(errorMessage);
                createdMessage = errorMessage;
            }

            _logger.LogInformation("Verificar se o médico já tem agendamento para o mesmo dia");
            var doctor = await _repository.GetDoctorById(request.DoctorId, cancellationToken);
            var doctorAlreadyScheduled = await _repository.DoctorAlreadyScheduled(request.DoctorId, scheduling.Value!.DateScheduling, cancellationToken);
            if (doctorAlreadyScheduled)
            {
                var errorMessage = $"O Doutor {doctor!.Name} já tem uma consulta agendada para este dia e horário: {scheduling.Value!.DateScheduling}";
                _logger.LogInformation(errorMessage);
                errors.Add(errorMessage);
                createdMessage = errorMessage;
            }

            if (errors.Count == 0)
            {
                _logger.LogInformation("Agendamento para o paciente {patient} com o médico {doctor} criado", patient!.Name, doctor!.Name);
                await _repository.CreateAsync(scheduling.Value!, cancellationToken);

                _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
                scheduling.Value!.ChangeObservation($"Consulta agendada com sucesso na data {scheduling.Value!.DateScheduling} com o médico {doctor!.Name}. Atente para o dia e horário");
                createdMessage = $"Olá {patient!.Name}. {scheduling.Value!.Observation}";
                var mailScheduling = new SendMailMessage(patient.Name!, patient.Email!, createdMessage);
                var message = new MessagePayload<SendMailMessage>(mailScheduling);
                _message.Publish(message, QUEUE);

                _logger.LogInformation("Salvando agendamento no banco de dados");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var mailScheduling = new SendMailMessage(patient!.Name!, patient.Email!, createdMessage);
                var message = new MessagePayload<SendMailMessage>(mailScheduling);
                _message.Publish(message, QUEUE);
            }

            return errors.Count != 0
                ? ValueResult.Failure(errors)
                : ValueResult.Success();
        }
    }
}