using Clinica.Main.Domain.Doctors;
using Clinica.Main.Domain.Patients;
using Clinica.Main.Domain.Schedulings;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Main.Infrastructure
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasKey(p => p.Id);

            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);

            modelBuilder.Entity<Specialty>().HasKey(s => s.Id);

            modelBuilder.Entity<Scheduling>().HasKey(s => s.Id);
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialty> Appointments { get; set; }

        public DbSet<Scheduling> Schedulings { get; set; }
    }
}