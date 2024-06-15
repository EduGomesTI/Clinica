using Clinica.Base.Domain;
using Clinica.Main.Application.Specialty.Responses;
using MediatR;

namespace Clinica.Main.Application.Specialty.Queries
{
    public sealed record GetSpecialtyByIdQuery(Guid Id) : IRequest<ValueResult<GetSpecialtyResponse>>;
}