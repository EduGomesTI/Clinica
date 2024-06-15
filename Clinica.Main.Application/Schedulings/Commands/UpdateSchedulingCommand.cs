using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Main.Application.Schedulings.Commands
{
    public sealed record UpdateSchedulingCommand(
        Guid Id,
        string Status,
        DateTime DateScheduling) : IRequest<ValueResult>;
}