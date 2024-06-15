using Clinica.Base.Domain;

namespace Clinica.Doctors.Domain.Aggregates
{
    public sealed class Specialty : AggregateRoot
    {
        public string? Description { get; private set; }

        public bool IsDeleted { get; private set; }

        private Specialty()
        {
        }

        private Specialty(string description)
        {
            Description = description;
            IsDeleted = false;
        }

        public static ValueResult<Specialty> Create(string description)
        {
            var errors = new List<ValueErrorDetail>();

            if (string.IsNullOrWhiteSpace(description))
                errors.Add(new ValueErrorDetail("Description", "Descrição é obrigatória"));

            var specialty = new Specialty(description);

            return ValueResult<Specialty>.Success(specialty);
        }

        public ValueResult UpdateDescription(string description)
        {
            var errors = new List<ValueErrorDetail>();

            if (string.IsNullOrWhiteSpace(description))
                errors.Add(new ValueErrorDetail("Description", "Descrição é obrigatória"));

            if (errors.Any())
                return ValueResult.Failure(errors);

            Description = description;

            return ValueResult.Success();
        }

        public void SofDelete(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}