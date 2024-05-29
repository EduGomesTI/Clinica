using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Specialty.Commands
{
    public sealed record SoftDeleteSpecialtyCommand(
        Guid Id,
        bool IsDeleted) : IRequest<ValueResult>;
}