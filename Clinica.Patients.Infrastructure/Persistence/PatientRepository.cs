using Clinica.Patients.Domain.Aggregates;
using Clinica.Patients.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Patients.Infrastructure.Persistence
{
    internal sealed class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Patient patient, CancellationToken cancellationToken)
        {
            await _context.AddAsync(patient, cancellationToken);
        }

        public void Delete(Guid patientId)
        {
            var patient = FindPatient(patientId);
            patient?.SofDelete(true);
        }

        public void Undelete(Guid patientId)
        {
            var patient = FindPatient(patientId);
            patient?.SofDelete(false);
        }

        public void Update(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
        }

        public bool ExistPatient(Guid patientId)
        {
            return _context.Patients.Any(p => p.Id == patientId);
        }

        public Patient FindPatient(Guid patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == patientId)!;
        }
    }
}