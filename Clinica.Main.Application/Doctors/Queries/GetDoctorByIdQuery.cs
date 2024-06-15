using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Responses;

using MediatR;

namespace Clinica.Main.Application.Doctors.Queries
{
    public sealed record GetDoctorByIdQuery(Guid Id) : IRequest<ValueResult<GetDoctorResponse>>;
}