using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Repository.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.AddressText)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Complement)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasMany(e => e.Donations)
                .WithOne(d => d.BillingAddress)
                .HasForeignKey(d => d.BillingAddressId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Address");
        }
    }
}