using Clinica.Main.Application.Specialty.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Specialty.Validators
{
    internal class SoftDeleteSpecialtyCommandValidator : AbstractValidator<SoftDeleteSpecialtyCommand>
    {
        public SoftDeleteSpecialtyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id é obrigatório");
            RuleFor(x => x.IsDeleted)
                .NotEmpty()
                .WithMessage("IsDeleted é obrigatório");
        }
    }
}
