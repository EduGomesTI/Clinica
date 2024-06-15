using Clinica.Doctors.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Doctors.Infrastructure.Persistence
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
    }
}