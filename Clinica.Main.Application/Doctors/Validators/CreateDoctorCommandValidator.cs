using Clinica.Main.Application.Doctors.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Doctors.Validators;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
        RuleFor(x => x.CRM).NotEmpty().WithMessage("Crm é obrigatório");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
    }
}
