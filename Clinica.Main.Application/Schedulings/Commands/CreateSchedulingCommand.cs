using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Schedulings.Commands
{
    public sealed record CreateSchedulingCommand(
        Guid PatientId,
        Guid DoctorId,
        DateTime DateScheduling) : IRequest<ValueResult>;
}