using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.DoctorsSchedule.Commands
{
    public sealed record UpdateDoctorScheduleCommand(
        Guid Id,
        Guid idDoctor,
        string WeekDay,
        string HourDay) : IRequest<ValueResult>;
}