using Clinica.Patients.Domain.Aggregates;

namespace Clinica.Patients.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task CreateAsync(Patient patient, CancellationToken cancellationToken);

        void Delete(Guid patientId);

        void Undelete(Guid patientId);

        void Update(Patient patient);

        Patient FindPatient(Guid patientId);

        bool ExistPatient(Guid patientId);
    }
}