using Clinica.Base.Domain;
using Clinica.Main.Application.DoctorsSchedule.Responses;
using MediatR;

namespace Clinica.Main.Application.DoctorsSchedule.Commands
{
    public sealed record UpdateDoctorScheduleCommand(
        Guid Id,
        string WeekDay,
        string HourDay) : IRequest<ValueResult>;
}