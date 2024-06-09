using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Doctors.Application.Commands
{
    public sealed record SoftDeleteSpecialtyCommand(
        Guid Id,
        bool IsDeleted) : IRequest<ValueResult>;
}