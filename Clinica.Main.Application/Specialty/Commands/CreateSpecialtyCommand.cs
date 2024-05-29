using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Specialty.Commands
{
    public sealed record CreateSpecialtyCommand(
        string Specialty) : IRequest<ValueResult>;
}