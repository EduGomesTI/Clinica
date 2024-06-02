using Clinica.Main.Application.Patients.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Patients.Validators
{
    internal class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id é obrigatório");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Telefone é obrigatório");
            RuleFor(x => x.BirthDate)
                .NotNull()
                .WithMessage("Data de nascimento é obrigatória");
        }
    }
}
