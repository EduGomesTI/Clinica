using Clinica.Doctors.Domain.Aggregates;
using Clinica.Doctors.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Doctors.Infrastructure.Persistence
{
    internal sealed class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorDbContext _context;

        public DoctorRepository(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Doctor entity, CancellationToken cancellationToken)
        {
            await _context.Doctors.AddAsync(entity, cancellationToken);
        }

        public void Delete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(true);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public bool Exist(Guid entityId)
        {
            return _context.Doctors.Any(e => e.Id == entityId);
        }

        public Doctor? Find(Guid entityId)
        {
            return _context
                .Doctors
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == entityId);
        }

        public void Undelete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(false);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public void Update(Doctor entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}