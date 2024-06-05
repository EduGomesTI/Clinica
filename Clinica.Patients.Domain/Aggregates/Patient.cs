using Clinica.Base.Domain;

namespace Clinica.Patients.Domain.Aggregates
{
    public sealed class Patient : AggregateRoot
    {
        public string? Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string? Email { get; private set; }

        public string? Phone { get; private set; }

        public string? Address { get; private set; }

        public bool IsDeleted { get; private set; }

        private Patient()
        { }

        public static ValueResult<Patient> Create(
            string name,
            DateTime birthDate,
            string email,
            string phone,
            string address)
        {
            var errors = new List<ValueErrorDetail>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add(new ValueErrorDetail("Name", "Nome é obrigatório"));

            if (birthDate == DateTime.MinValue || birthDate == DateTime.Now)
            {
                errors.Add(new ValueErrorDetail("Birthdate", $"Data de nascimento inválido: {birthDate.ToString()}"));
            }

            if (string.IsNullOrWhiteSpace(email))
                errors.Add(new ValueErrorDetail("Email", "Email é obrigatório"));

            if (string.IsNullOrWhiteSpace(phone))
                errors.Add(new ValueErrorDetail("Phone", "Telefone é obrigatório"));

            return errors.Count != 0
                ? ValueResult<Patient>.Failure(errors)
                : ValueResult<Patient>.Success(new Patient
                {
                    Name = name,
                    BirthDate = birthDate,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    IsDeleted = false
                });
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void UpdatePhone(string phone)
        {
            Phone = phone;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
        }

        public void SofDelete(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}