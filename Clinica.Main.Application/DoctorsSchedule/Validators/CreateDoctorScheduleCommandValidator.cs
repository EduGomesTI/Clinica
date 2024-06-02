using Clinica.Main.Application.DoctorsSchedule.Commands;
using FluentValidation;

namespace Clinica.Main.Application.DoctorsSchedule.Validators
{
    internal class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
    {
        public CreateDoctorScheduleCommandValidator()
        {
            RuleFor(x => x.idDoctor).NotEmpty().NotNull().WithMessage("idDoctor é obrigatório");
            RuleFor(x => x.HourDay).NotEmpty().WithMessage("HourDay é obrigatório");
            RuleFor(x => x.WeekDay).NotEmpty().WithMessage("WeekDay é obrigatório");
        }
    }
}
