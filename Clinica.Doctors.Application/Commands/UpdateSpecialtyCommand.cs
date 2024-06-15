using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Doctors.Application.Commands
{
    public sealed record UpdateSpecialtyCommand(
        Guid Id,
        string Specialty) : IRequest<ValueResult>;
}