using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Patients.Commands
{
    public sealed record CreatePatientCommand(
        string Name,
        DateTime BirthDate,
        string Email,
        string Phone,
        string Adrress) : IRequest<ValueResult>;
}