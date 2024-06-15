using Clinica.Base.Domain;

namespace Clinica.Main.Domain.Doctors
{
    public sealed class Doctor : AggregateRoot
    {
        public string? Name { get; set; }

        public string? CRM { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public Guid IdSpecialty { get; set; }

        public bool IsDeleted { get; set; }
    }
}