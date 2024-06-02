using Clinica.Main.Application.Schedulings.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Schedulings.Validators
{
    internal class UpdateSchedulingCommandValidator : AbstractValidator<UpdateSchedulingCommand>
    {
        public UpdateSchedulingCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id é obrigatório");
            RuleFor(x => x.Status).NotNull().NotEmpty().WithMessage("Status é obrigatório");
        }
    }
}
