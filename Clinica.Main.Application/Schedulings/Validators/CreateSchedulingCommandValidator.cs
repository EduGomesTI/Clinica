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
                .GreaterThan(DateTime.Now)
                .WithMessage("A data da consulta não pode ser menor que a data atual")
                .Must(BeWithinAllowedHours)
                .WithMessage("A consulta deve ser agendada entre 8h e 20h");
        }

        private bool BeWithinAllowedHours(DateTime dateScheduling)
        {
            var time = dateScheduling.TimeOfDay;
            return time >= new TimeSpan(8, 0, 0) && time < new TimeSpan(20, 0, 0);
        }
    }
}