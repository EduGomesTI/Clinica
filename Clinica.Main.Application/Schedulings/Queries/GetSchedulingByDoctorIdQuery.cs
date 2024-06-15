using Clinica.Base.Domain;
using Clinica.Main.Application.Scheduling.Responses;
using MediatR;

namespace Clinica.Main.Application.Schedulings.Queries
{
    public sealed record GetSchedulingByDoctorIdQuery(
        Guid DoctorId) : IRequest<ValueResult<IEnumerable<GetSchedulingResponse>>>;
}