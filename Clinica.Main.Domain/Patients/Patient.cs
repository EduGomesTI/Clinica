using Clinica.Base.Domain;

namespace Clinica.Main.Domain.Patients
{
    public sealed class Patient : AggregateRoot
    {
        public string? Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool IsDeleted { get; set; }

        public Patient()
        { }
    }
}