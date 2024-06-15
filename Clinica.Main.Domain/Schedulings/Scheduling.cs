using Clinica.Base.Domain;

namespace Clinica.Main.Domain.Schedulings
{
    public sealed class Scheduling : AggregateRoot
    {
        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public DateTime DateScheduling { get; set; }

        public AppointmentStatus Status { get; set; }

        public string? Observation { get; set; }
    }
}