using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Doctors.Application.Commands
{
    public sealed record CreateDoctorCommand(
        string Name,
        string CRM,
        DateTime BirthDate,
        string Email,
        string Phone,
        string Address,
        Guid IdSpecialty) : IRequest<ValueResult>;
}