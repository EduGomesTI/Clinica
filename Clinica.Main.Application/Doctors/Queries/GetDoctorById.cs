using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Responses;

using MediatR;

namespace Clinica.Main.Application.Doctors.Queries
{
    public sealed record GetDoctorById(Guid Id) : IRequest<ValueResult<GetDoctorResponse>>;
}