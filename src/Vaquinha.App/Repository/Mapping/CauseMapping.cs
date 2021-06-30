using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Repository.Mapping
{
    public class CauseMapping : IEntityTypeConfiguration<Cause>
    {
        public void Configure(EntityTypeBuilder<Cause> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(150);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(2);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Cause");
        }
    }
}