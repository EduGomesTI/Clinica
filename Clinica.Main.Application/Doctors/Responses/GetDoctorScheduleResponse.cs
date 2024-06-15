namespace Clinica.Main.Application.Doctors.Responses
{
    public sealed record GetDoctorScheduleResponse(string PatientName, DateTime DateScheduling);
}