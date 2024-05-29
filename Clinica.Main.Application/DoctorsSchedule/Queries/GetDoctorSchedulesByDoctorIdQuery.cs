using Clinica.Base.Domain;
using Clinica.Main.Application.DoctorsSchedule.Responses;
using MediatR;

namespace Clinica.Main.Application.DoctorsSchedule.Queries
{
    public sealed record GetDoctorSchedulesByDoctorIdQuery(Guid DoctorId) : IRequest<ValueResult<IEnumerable<GetDoctorScheduleResponse>>>;
}