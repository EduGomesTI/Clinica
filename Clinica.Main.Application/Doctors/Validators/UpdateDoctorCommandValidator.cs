using Clinica.Main.Application.Doctors.Commands;
using FluentValidation;

namespace Clinica.Main.Application.Doctors.Validators
{
    internal class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id né obrigatório");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.CRM).NotEmpty().WithMessage("Crm é obrigatório");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
        }
    }
}
