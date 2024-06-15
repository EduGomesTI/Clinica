using Clinica.Patients.Domain.Aggregates;
using Clinica.Patients.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Patients.Infrastructure.Persistence
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Patient entity, CancellationToken cancellationToken)
        {
            await _context.Patients.AddAsync(entity, cancellationToken);
        }

        public void Delete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(true);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public bool Exist(Guid entityId)
        {
            return _context.Patients.Any(e => e.Id == entityId);
        }

        public Patient? Find(Guid entityId)
        {
            return _context
                .Patients
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == entityId);
        }

        public void Undelete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(false);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public void Update(Patient entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}