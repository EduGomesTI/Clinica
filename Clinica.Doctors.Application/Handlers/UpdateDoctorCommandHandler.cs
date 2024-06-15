using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Mail;
using Clinica.Doctors.Application.Commands;
using Clinica.Doctors.Domain.Repositories;
using Clinica.Doctors.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Doctors.Application.Handlers
{
    public sealed class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, ValueResult>
    {
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly IDoctorRepository _repository;
        private readonly ILogger<UpdateDoctorCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;
        private readonly IMessageService _message;

        public UpdateDoctorCommandHandler(
            IDoctorRepository repository,
            ILogger<UpdateDoctorCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork,
            IMessageService message)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _message = message;
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

            _logger.LogInformation("Atualizar médico {Name}", request.Name);
            doctor.UpdateName(request.Name);
            doctor.UpdateBirthDate(request.BirthDate);
            doctor.UpdateEmail(request.Email);
            doctor.UpdatePhone(request.Phone);
            doctor.UpdateAddress(request.Address);
            doctor.UpdateSpecialty(request.IdSpecialty);

            _logger.LogInformation("Enviar alteração para o DbContext");
            _repository.Update(doctor!);

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            var updatedMessage = $"Olá {doctor.Name}. Seus dados foram atualizados com sucesso.";
            var messagePayload = new SendMailMessage(doctor.Name!, doctor.Email!, updatedMessage);
            var message = new MessagePayload<SendMailMessage>(messagePayload);
            _message.Publish(message, QUEUE);

            _logger.LogInformation("Salvando alterações no banco de dados");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}