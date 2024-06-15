using Clinica.Base.Application;
using Clinica.Base.Domain;
using Clinica.Base.Infrastructure.Brokes;
using Clinica.Base.Infrastructure.Brokes.RabbitMq;
using Clinica.Base.Infrastructure.Consts;
using Clinica.Base.Infrastructure.Mail;
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
        private const string QUEUE = MessageConstants.clinic_mail_service;
        private readonly IDoctorRepository _repository;
        private readonly ILogger<CreateDoctorCommandHandler> _logger;
        private readonly IUnitOfWork<DoctorDbContext> _unitOfWork;
        private readonly IMessageService _message;

        public CreateDoctorCommandHandler(
            IDoctorRepository repository,
            ILogger<CreateDoctorCommandHandler> logger,
            IUnitOfWork<DoctorDbContext> unitOfWork,
            IMessageService message)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _message = message;
        }

        public async Task<ValueResult> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating doctor {Name}", request.Name);
            var doctor = Doctor.Create(request.Name, request.CRM, request.BirthDate, request.Email,
                request.Phone, request.Address, request.IdSpecialty);

            if (!doctor.Succeeded)
            {
                _logger.LogError("Error creating doctor {Name}", request.Name);
                return ValueResult.Failure(doctor.ErrorDetails!);
            }

            _logger.LogInformation("Atualizar DbContext");
            await _repository.CreateAsync(doctor.Value!, cancellationToken);

            _logger.LogInformation($"Enviar mensagem para a fila {QUEUE}");
            var createdMessage = $"Olá {doctor.Value!.Name}. Seja bem-vindo!";
            var mailDoctor = new SendMailMessage(doctor.Value!.Name!, doctor.Value!.Email!, createdMessage);
            var message = new MessagePayload<SendMailMessage>(mailDoctor);
            _message.Publish(message, QUEUE);

            _logger.LogInformation("Salvar alterações no banco de dados");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ValueResult.Success();
        }
    }
}