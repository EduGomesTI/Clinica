using Clinica.Base.Domain;
using Clinica.Main.Application.Patients.Responses;
using MediatR;

namespace Clinica.Main.Application.Patients.Commands
{
    public sealed record LoginPatientCommand(string Email, string Password = "12345") : IRequest<ValueResult<LoginPatientResponse>>
    {
    }
}