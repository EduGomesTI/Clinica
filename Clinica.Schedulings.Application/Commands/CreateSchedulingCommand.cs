using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Schedulings.Application.Commands
{
    public sealed record CreateSchedulingCommand(
        Guid PatientId,
        Guid DoctorId,
        DateTime DateScheduling) : IRequest<ValueResult>;
}