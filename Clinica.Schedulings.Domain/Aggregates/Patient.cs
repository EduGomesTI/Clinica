using Clinica.Base.Domain;

namespace Clinica.Schedulings.Domain.Aggregates
{
    public sealed class Patient : ValueObject
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool IsDeleted { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object> { Name!, BirthDate, Email!, Phone!, Address! };
        }
    }
}