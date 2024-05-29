using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Schedulings.Commands
{
    public sealed record UpdateSchedulingCommand(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        DateTime DateScheduling,
        string Status) : IRequest<ValueResult>;
}