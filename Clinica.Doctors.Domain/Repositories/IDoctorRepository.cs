using Clinica.Doctors.Domain.Aggregates;

namespace Clinica.Doctors.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task CreateAsync(Doctor entity, CancellationToken cancellationToken);

        void Delete(Guid entityId);

        void Undelete(Guid entityId);

        void Update(Doctor entity);

        Doctor? Find(Guid entityId);

        bool Exist(Guid entityId);
    }
}