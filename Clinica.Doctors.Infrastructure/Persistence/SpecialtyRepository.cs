using Clinica.Doctors.Domain.Aggregates;
using Clinica.Doctors.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Doctors.Infrastructure.Persistence
{
    internal sealed class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly DoctorDbContext _context;

        public SpecialtyRepository(DoctorDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Specialty entity, CancellationToken cancellationToken)
        {
            await _context.Specialties.AddAsync(entity, cancellationToken);
        }

        public void Delete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(true);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public bool Exist(Guid entityId)
        {
            return _context.Specialties.Any(e => e.Id == entityId);
        }

        public Specialty? Find(Guid entityId)
        {
            return _context
                .Specialties
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == entityId);
        }

        public void Undelete(Guid entityId)
        {
            var entity = Find(entityId);
            entity?.SofDelete(false);
            _context.Entry(entity!).State = EntityState.Modified;
        }

        public void Update(Specialty entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}