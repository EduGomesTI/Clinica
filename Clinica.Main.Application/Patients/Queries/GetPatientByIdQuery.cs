using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Responses;

using MediatR;

namespace Clinica.Main.Application.Patients.Queries
{
    public sealed record GetPatientByIdQuery(Guid Id) : IRequest<ValueResult<GetPatientResponse>>;
}