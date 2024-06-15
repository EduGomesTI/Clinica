using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Doctors.Application.Commands
{
    public sealed record SoftDeleteDoctorCommand(
        Guid IdDoctor,
        bool IsDeleted) : IRequest<ValueResult>;
}