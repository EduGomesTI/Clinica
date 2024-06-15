using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Specialty.Commands
{
    public sealed record UpdateSpecialtyCommand(
        Guid Id,
        string Specialty) : IRequest<ValueResult>;
}