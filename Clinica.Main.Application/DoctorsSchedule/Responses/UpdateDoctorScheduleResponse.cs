namespace Clinica.Main.Application.DoctorsSchedule.Responses
{
    public sealed record UpdateDoctorScheduleResponse(
        Guid Id,
        string WeekDay,
        string HourDay
        );
}