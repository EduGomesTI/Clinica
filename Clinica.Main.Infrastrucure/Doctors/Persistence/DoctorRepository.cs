using Clinica.Main.Domain.Doctors;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Main.Infrastructure.Doctors.Persistence
{
    internal class DoctorRepository : IDoctorRepository
    {
        private readonly MainDbContext _context;

        public DoctorRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, DateTime>> GetDoctorScheduler(Guid? DoctorId, CancellationToken cancellationToken)
        {
            var schedulings = await _context.Schedulings
                        .AsNoTracking()
                        .Where(s => s.DoctorId == DoctorId)
                        .Join(_context.Patients,
                              s => s.PatientId,
                              p => p.Id,
                              (s, p) => new { p.Name, s.DateScheduling })
                        .ToListAsync(cancellationToken);

            var schedulerDictionary = schedulings.ToDictionary(s => s.Name!, s => s.DateScheduling);

            return schedulerDictionary!;
        }
    }
}