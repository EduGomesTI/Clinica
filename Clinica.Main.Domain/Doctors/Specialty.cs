using Clinica.Base.Domain;

namespace Clinica.Main.Domain.Doctors
{
    public sealed class Specialty : AggregateRoot
    {
        public string? Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}