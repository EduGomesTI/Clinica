using Clinica.Main.Application.Schedulings.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Schedulings.Validators
{
    internal class CreateSchedulingCommandValidator : AbstractValidator<CreateSchedulingCommand>
    {
        public CreateSchedulingCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                .WithMessage("DoctorId é obrigatório");
            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithMessage("PatientId é obrigatório");
            RuleFor(x => x.DateScheduling)
                .NotEmpty()
                .WithMessage("DateScheduling é obrigatório");

        }
    }
}
