using Clinica.Patients.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Patients.Infrastructure.Persistence
{
    internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .IsRequired();

            builder.Property(x => x.Address)
                .HasColumnName("Address")
                .HasColumnType("varchar(100)");

            builder.Property(x => x.IsDeleted)
                .HasColumnName("IsDeleted")
                .IsRequired();
        }
    }
}