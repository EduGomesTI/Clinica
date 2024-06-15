using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Doctors.Application.Commands
{
    public sealed record CreateSpecialtyCommand(
        string Specialty) : IRequest<ValueResult>;
}