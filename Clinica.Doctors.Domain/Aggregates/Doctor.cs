using Clinica.Base.Domain;

namespace Clinica.Doctors.Domain.Aggregates
{
    public sealed class Doctor : AggregateRoot
    {
        public string? Name { get; private set; }

        public string? CRM { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string? Email { get; private set; }

        public string? Phone { get; private set; }

        public string? Address { get; private set; }

        public Guid IdSpecialty { get; private set; }

        public bool IsDeleted { get; private set; }

        private Doctor()
        {
        }

        private Doctor(string name, string crm, DateTime birthDate,
            string email, string phone, string address,
            Guid idSpecialty)
        {
            Name = name;
            CRM = crm;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
            Address = address;
            IdSpecialty = idSpecialty;
            IsDeleted = false;
        }

        public static ValueResult<Doctor> Create(
            string name, string crm, DateTime birthDate,
            string email, string phone, string address,
            Guid idSpecialty)
        {
            var errors = new List<ValueErrorDetail>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add(new ValueErrorDetail("Name", "Nome é obrigatório"));

            if (string.IsNullOrWhiteSpace(crm))
                errors.Add(new ValueErrorDetail("CRM", "CRM é obrigatório"));

            if (birthDate == DateTime.MinValue || birthDate == DateTime.Now)
                errors.Add(new ValueErrorDetail("Birthdate", $"Data de nascimento inválido: {birthDate.ToString()}"));

            if (string.IsNullOrWhiteSpace(email))
                errors.Add(new ValueErrorDetail("Email", "Email é obrigatório"));

            if (string.IsNullOrWhiteSpace(phone))
                errors.Add(new ValueErrorDetail("Phone", "Telefone é obrigatório"));

            var doctor = new Doctor(name, crm, birthDate, email, phone, address, idSpecialty);
            return errors.Count != 0
                ? ValueResult<Doctor>.Failure(errors)
                : ValueResult<Doctor>.Success(doctor);
        }

        public ValueResult UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValueResult.Failure("Nome é obrigatório");
            Name = name;
            return ValueResult.Success();
        }

        public ValueResult UpdateCrm(string crm)
        {
            if (string.IsNullOrWhiteSpace(crm))
                return ValueResult.Failure("Nome é obrigatório");
            CRM = crm;
            return ValueResult.Success();
        }

        public ValueResult UpdateBirthDate(DateTime birthDate)
        {
            if (birthDate == DateTime.MinValue || birthDate == DateTime.Now)
                return ValueResult.Failure("Birthdate", $"Data de nascimento inválido: {birthDate}");
            BirthDate = birthDate;
            return ValueResult.Success();
        }

        public ValueResult UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ValueResult.Failure("Email é obrigatório");
            Email = email;
            return ValueResult.Success();
        }

        public ValueResult UpdatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return ValueResult.Failure("Telefone é obrigatório");
            Phone = phone;
            return ValueResult.Success();
        }

        public ValueResult UpdateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return ValueResult.Failure("Telefone é obrigatório");
            Address = address;
            return ValueResult.Success();
        }

        public ValueResult UpdateSpecialty(Guid idSpecialty)
        {
            IdSpecialty = idSpecialty;
            return ValueResult.Success();
        }

        public void SofDelete(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}