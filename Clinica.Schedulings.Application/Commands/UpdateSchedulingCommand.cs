using Clinica.Base.Domain;
using MediatR;

namespace Clinica.Schedulings.Application.Commands
{
    public sealed record UpdateSchedulingCommand(
        Guid Id,
        string Status,
        DateTime DateScheduling) : IRequest<ValueResult>;
}