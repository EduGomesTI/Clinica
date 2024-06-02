using Clinica.Main.Application.DoctorsSchedule.Commands;
using FluentValidation;

namespace Clinica.Main.Application.DoctorsSchedule.Validators
{
    internal class SoftDeleteDoctorScheduleCommandValidator : AbstractValidator<SoftDeleteDoctorScheduleCommand>
    {
        public SoftDeleteDoctorScheduleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id Não pode ficar nulo");
            RuleFor(x => x.IsDeleted).NotEmpty().NotNull().WithMessage("IsDeleted é obrigatório");
        }
    }
}
