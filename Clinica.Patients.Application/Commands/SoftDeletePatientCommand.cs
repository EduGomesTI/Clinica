using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Patients.Commands
{
    public sealed record SoftDeletePatientCommand(
        Guid Id,
        bool IsDeleted) : IRequest<ValueResult>;
}