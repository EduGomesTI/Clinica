using Clinica.Base.Application;

namespace Clinica.Main.Application.Patients.Responses
{
    public sealed record GetPatientResponse(Guid Id,
        string Name,
        DateTime BirthDate,
        string Email,
        string Phone,
        string Adrress);
}