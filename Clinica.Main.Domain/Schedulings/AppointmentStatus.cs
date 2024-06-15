namespace Clinica.Main.Domain.Schedulings
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