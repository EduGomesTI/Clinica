using Clinica.Main.Application.Patients.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Patients.Validators
{
    internal class SoftDeletePatientCommandValidator : AbstractValidator<SoftDeletePatientCommand>
    {
        public SoftDeletePatientCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id Não pode ficar nulo");
        }
    }
}