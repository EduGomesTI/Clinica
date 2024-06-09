using Clinica.Doctors.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Doctors.Infrastructure.Persistence
{
    internal sealed class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialties");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}