using Clinica.Schedulings.Domain.Aggregates;
using Clinica.Schedulings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Schedulings.Infrastructure.Persistence
{
    internal class SchedulingRepository : ISchedulingRepository
    {
        private readonly SchedulingDbContext _context;

        public SchedulingRepository(SchedulingDbContext context)
        {
            _context = context;
        }

        public void ChangeStatus(Guid entityId, AppointmentStatus status)
        {
            var entity = Find(entityId);
            switch (status)
            {
                case AppointmentStatus.Confirmed:
                    entity?.Confirm();
                    break;

                case AppointmentStatus.CancelledByPatient:
                    entity?.CancelByPatient();
                    break;

                case AppointmentStatus.CancelledByDoctor:
                    entity?.CancelByDoctor();
                    break;

                case AppointmentStatus.Completed:
                    entity?.Complete();
                    break;
            }
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public async Task CreateAsync(Scheduling entity, CancellationToken cancellationToken)
        {
            await _context.Schedulings.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> DoctorAlreadyScheduled(Guid entityId, DateTime dateScheduling, CancellationToken cancellationToken)
        {
            return await _context
                .Schedulings
                .AnyAsync(e => e.DoctorId == entityId && e.DateScheduling == dateScheduling, cancellationToken);
        }

        public bool Exist(Guid entityId)
        {
            return _context.Schedulings.Any(e => e.Id == entityId);
        }

        public Scheduling? Find(Guid entityId)
        {
            return _context
                .Schedulings
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == entityId);
        }

        public async Task<Doctor?> GetDoctorById(Guid entityId, CancellationToken cancellationToken)
        {
            return await _context
                .Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken);
        }

        public async Task<Patient?> GetPatientById(Guid entityId, CancellationToken cancellationToken)
        {
            return await _context
                .Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken);
        }

        public async Task<bool> PatientAlreadyScheduled(Guid entityId, DateTime dateScheduling, CancellationToken cancellationToken)
        {
            return await _context
                .Schedulings
                .AnyAsync(e => e.PatientId == entityId && e.DateScheduling == dateScheduling, cancellationToken);
        }

        public void Update(Scheduling entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}