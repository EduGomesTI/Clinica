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
    public sealed class UpdateSchedulingCommandHandler : IRequestHandler<UpdateSchedulingCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly ISchedulingRepository _repository;
        private readonly ILogger<UpdateSchedulingCommandHandler> _logger;
        private readonly IMessageService _message;
        private readonly IUnitOfWork<SchedulingDbContext> _unitOfWork;

        public UpdateSchedulingCommandHandler(
            ILogger<UpdateSchedulingCommandHandler> logger
            , IMessageService message,
            ISchedulingRepository repository,
            IUnitOfWork<SchedulingDbContext> unitOfWork)
        {
            _logger = logger;
            _message = message;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValueResult> Handle(UpdateSchedulingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Retornar Agendamento do banco.");
            var scheduling = _repository.Find(request.Id);

            ValueResult<Scheduling> result = default;

            _logger.LogWarning("Alterar status do agendamento.");
            _ = Enum.TryParse<AppointmentStatus>(request.Status, out var status);
            switch (status)
            {
                case AppointmentStatus.Confirmed:
                    result = scheduling!.Confirm();
                    break;

                case
                    AppointmentStatus.ReScheduling:
                    result = scheduling!.Reschedule(request.DateScheduling);
                    break;

                case AppointmentStatus.CancelledByPatient:
                    result = scheduling!.CancelByPatient();
                    break;

                case AppointmentStatus.CancelledByDoctor:
                    result = scheduling!.CancelByDoctor();
                    break;

                case AppointmentStatus.Completed:
                    result = scheduling!.Complete();
                    break;

                case AppointmentStatus.NoShow:
                    result = scheduling!.NoShow();
                    break;
            }

            var patient = await _repository.GetPatientById(scheduling!.PatientId, cancellationToken);
            var mailScheduling = new SendMailMessage(patient!.Name!, patient.Email!, scheduling.Observation!);
            var message = new MessagePayload<SendMailMessage>(mailScheduling);

            if (result.ErrorDetails!.Count > 0)
            {
                _message.Publish(message, QUEUE);
                return ValueResult.Failure(result.ErrorDetails);
            }

            _logger.LogWarning("Atualizar agendamento no banco.");
            _repository.Update(scheduling!);

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            _message.Publish(message, QUEUE);

            _logger.LogWarning("Salvar alterações no banco.");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}