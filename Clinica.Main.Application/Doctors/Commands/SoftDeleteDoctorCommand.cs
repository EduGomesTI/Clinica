using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Doctors.Commands
{
    public sealed record SoftDeleteDoctorCommand(
        Guid IdDoctor,
        bool IsDeleted) : IRequest<ValueResult>;
}