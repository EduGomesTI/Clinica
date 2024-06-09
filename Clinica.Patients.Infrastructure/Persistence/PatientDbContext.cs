using Clinica.Patients.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Patients.Infrastructure.Persistence
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
    }
}