using Clinica.Doctors.Domain.Aggregates;

namespace Clinica.Doctors.Domain.Repositories
{
    public interface ISpecialtyRepository
    {
        Task CreateAsync(Specialty entity, CancellationToken cancellationToken);

        void Delete(Guid entityId);

        void Undelete(Guid entityId);

        void Update(Specialty entity);

        Specialty? Find(Guid entityId);

        bool Exist(Guid entityId);
    }
}