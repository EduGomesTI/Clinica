namespace Clinica.Main.Application.DoctorsSchedule.Responses
{
    public sealed record GetDoctorScheduleResponse(
        Guid Id,
        string WeekDay,
        string HourDay
        );
}