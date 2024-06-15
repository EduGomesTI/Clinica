using Clinica.Schedulings.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Schedulings.Infrastructure.Persistence
{
    public class SchedulingDbContext : DbContext
    {
        public SchedulingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Scheduling> Schedulings { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
    }
}