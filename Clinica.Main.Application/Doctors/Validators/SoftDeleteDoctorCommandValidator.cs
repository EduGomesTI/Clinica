using Clinica.Main.Application.Doctors.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Doctors.Validators
{
    internal class SoftDeleteDoctorCommandValidator : AbstractValidator<SoftDeleteDoctorCommand>
    {
        public SoftDeleteDoctorCommandValidator()
        {
            RuleFor(x => x.IdDoctor).NotEmpty().NotNull().WithMessage("Id é obrigatório.");
            RuleFor(x => x.IsDeleted).NotNull().WithMessage("IsDeleted é obrigatório. Escolha entre True ou False");
        }
    }
}
