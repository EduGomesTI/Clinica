using Clinica.Main.Domain.Patients;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Main.Infrastructure.Patients.Persistence
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly MainDbContext _context;

        public PatientRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(CancellationToken cancellationToken, bool page = false, int pageStart = 1, int pageSize = 1)
        {
            return await _context
                .Patients
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageStart - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken)
        {
            return await _context
                .Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
        }

        public async Task<Patient?> GetPatientByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return await _context
                .Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}