using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Specialty.Validators
{
    internal class CreateSpecialtyCommandValidator : AbstractValidator<CreateSpecialtyCommand>
    {
        public CreateSpecialtyCommandValidator()
        {
            RuleFor(x => x.Specialty)
    .NotEmpty()
    .WithMessage("Especialidade é obrigatório")
    .MaximumLength(30)
    .WithMessage("Especailidade deve ter no máximo 30 caracteres");
        }
    }
}
