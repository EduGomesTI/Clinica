using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Specialty.Validators
{
    internal class UpdateSpecialtyCommandValidator : AbstractValidator<UpdateSpecialtyCommand>
    {
        public UpdateSpecialtyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id é obrigatório");

            RuleFor(x => x.Specialty)
                .NotEmpty()
                .WithMessage("Especialidade é obrigatório")
                .MaximumLength(30)
                .WithMessage("Especailidade deve ter no máximo 30 caracteres");
        }
    }
}
