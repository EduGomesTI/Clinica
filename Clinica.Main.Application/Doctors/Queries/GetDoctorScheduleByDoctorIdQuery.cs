using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Responses;
using MediatR;

namespace Clinica.Main.Application.Doctors.Queries
{
    public sealed record GetDoctorScheduleByDoctorIdQuery(Guid Id) : IRequest<ValueResult<IEnumerable<GetDoctorScheduleResponse>>>;
}