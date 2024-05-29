namespace Clinica.Main.Application.Scheduling.Responses
{
    public sealed record GetSchedulingResponse(
        string PatientName,
        string DoctorName,
        int DayScheduling,
        int HourScheduling,
        string Status);
}