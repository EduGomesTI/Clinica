namespace Clinica.Schedulings.Domain.Aggregates
{
    public enum AppointmentStatus
    {
        Scheduled,
        Confirmed,
        ReScheduling,
        CancelledByPatient,
        CancelledByDoctor,
        Completed,
        NoShow
    }
}