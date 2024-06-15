using Clinica.Main.Application.Patients.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Patients.Validators
{
    internal sealed class LoginPatientCommandValidator : AbstractValidator<LoginPatientCommand>
    {
        public LoginPatientCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .EmailAddress()
                .MaximumLength(50)
                .WithMessage("E-mail não pode ter mais que 50 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório")
                .MaximumLength(5)
                .WithMessage("Senha não pode ter mais que 5 caracteres");
        }
    }
}