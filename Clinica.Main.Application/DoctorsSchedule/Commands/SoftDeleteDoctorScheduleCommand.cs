using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.DoctorsSchedule.Commands
{
    public sealed record SoftDeleteDoctorScheduleCommand(
        Guid Id,
        bool IsDeleted) : IRequest<ValueResult>;
}