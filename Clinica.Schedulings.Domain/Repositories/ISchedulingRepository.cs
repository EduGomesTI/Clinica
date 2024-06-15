using Clinica.Schedulings.Domain.Aggregates;

namespace Clinica.Schedulings.Domain.Repositories
{
    public interface ISchedulingRepository
    {
        Task CreateAsync(Scheduling entity, CancellationToken cancellationToken);

        void Update(Scheduling entity);

        Scheduling? Find(Guid entityId);

        bool Exist(Guid entityId);

        Task<Patient?> GetPatientById(Guid entityId, CancellationToken cancellationToken);

        Task<Doctor?> GetDoctorById(Guid entityId, CancellationToken cancellationToken);

        Task<bool> PatientAlreadyScheduled(Guid entityId, DateTime dateScheduling, CancellationToken cancellationToken);

        Task<bool> DoctorAlreadyScheduled(Guid entityId, DateTime dateScheduling, CancellationToken cancellationToken);
    }
}