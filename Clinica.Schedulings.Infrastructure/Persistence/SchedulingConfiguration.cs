using Clinica.Schedulings.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Schedulings.Infrastructure.Persistence
{
    internal class SchedulingConfiguration : IEntityTypeConfiguration<Scheduling>
    {
        public void Configure(EntityTypeBuilder<Scheduling> builder)
        {
            builder.ToTable("Schedulings");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.PatientId)
                .HasColumnName("PatientId")
                .IsRequired();

            builder.Property(x => x.DoctorId)
                .HasColumnName("DoctorId")
                .IsRequired();

            builder.Property(x => x.DateScheduling)
                .HasColumnName("DateScheduling")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Observation)
                .HasColumnName("Observation")
                .HasColumnType("varchar(255)");
        }
    }
}