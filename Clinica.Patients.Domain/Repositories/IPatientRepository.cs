using Clinica.Patients.Domain.Aggregates;

namespace Clinica.Patients.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task CreateAsync(Patient entity, CancellationToken cancellationToken);

        void Delete(Guid entityId);

        void Undelete(Guid entityId);

        void Update(Patient entity);

        Patient? Find(Guid entityId);

        bool Exist(Guid entityId);
    }
}