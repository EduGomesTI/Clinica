namespace Clinica.Main.Application.Doctors.Responses
{
    public sealed record GetDoctorResponse(
        Guid Id,
        string Name,
        string CRM,
        DateTime BirthDate,
        string Email,
        string Phone,
        string Address,
        string Specialty
        );
}