using Clinica.Main.Application.Patients.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Patients.Validators
{
    internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()                
                .WithMessage("Nome é obrigatório")
                .MaximumLength(50)
                .WithMessage("Nome nãoo pode ter mais que 50 caracteres");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório")
                .EmailAddress()
                .MaximumLength(50)
                .WithMessage("E-mail não pode ter mais que 50 caracteres");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Telefone é obrigatório");                
            RuleFor(x => x.BirthDate)
                .NotNull()
                .WithMessage("Data de nascimento é obrigatória");
            RuleFor(x => x.Adrress)
                .MaximumLength(100)
                .WithMessage("Endereço não pode ter mais que 100 caracteres");
        }
    }
}
